﻿using UnityEngine;
using System.Collections.Generic;

namespace PaintIn3D
{
	/// <summary>This base class allows you to easily create components that can have their paint lines connected together to form quads.</summary>
	public abstract class P3dConnectableLines : MonoBehaviour
	{
		class Link
		{
			public object  Owner;
			public Vector3 Position;
			public Vector3 EndPosition;
			public float   Age;
			public bool    Preview;
		}

		/// <summary>If you enable this then the hit points generated by this component will be connected into lines, allowing you to paint continuously.</summary>
		public bool ConnectHits { set { connectHits = value; } get { return connectHits; } } [SerializeField] protected bool connectHits;

		/// <summary>The world space distance between each paint point.
		/// 0 = No spacing.</summary>
		public float HitSpacing { set { hitSpacing = value; } get { return hitSpacing; } } [SerializeField] private float hitSpacing;

		/// <summary>When using <b>HitSpacing</b>, this prevents scenarios where something goes wrong and you attempt to paint too many times per frame.</summary>
		public int HitLimit { set { hitLimit = value; } get { return hitLimit; } } [SerializeField] private int hitLimit = 30;

		[System.NonSerialized]
		private List<Link> links = new List<Link>();

		[System.NonSerialized]
		private static Stack<Link> linkPool = new Stack<Link>();

		[System.NonSerialized]
		protected P3dHitCache hitCache = new P3dHitCache();

		public P3dHitCache HitCache
		{
			get
			{
				return hitCache;
			}
		}

		/// <summary>This component sends hit events to a cached list of components that can receive them. If this list changes then you must manually call this method.</summary>
		[ContextMenu("Clear Hit Cache")]
		public void ClearHitCache()
		{
			hitCache.Clear();
		}

		/// <summary>If this GameObject has teleported and you have <b>ConnectHits</b> or <b>HitSpacing</b> enabled, then you can call this to prevent a quad being drawn between the previous and current lines.</summary>
		[ContextMenu("Reset Connections")]
		public void ResetConnections()
		{
			for (var i = links.Count - 1; i >= 0; i--)
			{
				linkPool.Push(links[i]);
			}

			links.Clear();
		}

		protected void BreakHits(object owner)
		{
			for (var i = links.Count - 1; i >= 0; i--)
			{
				var link = links[i];

				if (link.Owner == owner)
				{
					links.RemoveAt(i);

					linkPool.Push(link);

					return;
				}
			}
		}

		protected void SubmitLine(bool preview, int priority, Vector3 position, Vector3 endPosition, Quaternion rotation, float pressure, object owner)
		{
			if (owner != null)
			{
				var link = default(Link);

				if (TryGetLink(owner, ref link) == true)
				{
					if (link.Preview == preview)
					{
						if (hitSpacing > 0.0f)
						{
							var currentPositionA = link.Position;
							var currentPositionB = link.EndPosition;
							var distanceA        = Vector3.Distance(link.Position, position);
							var distanceB        = Vector3.Distance(link.EndPosition, endPosition);
							var stepsA           = Mathf.FloorToInt(distanceA / hitSpacing);
							var stepsB           = Mathf.FloorToInt(distanceB / hitSpacing);

							if (stepsA > 0 || stepsB > 0)
							{
								var steps       = Mathf.Max(stepsA, stepsB);
								var hitSpacingA = hitSpacing;
								var hitSpacingB = hitSpacing;

								if (steps > hitLimit)
								{
									steps = hitLimit;
								}

								if (stepsA > stepsB)
								{
									hitSpacingB = (distanceB * (distanceA / (stepsA * hitSpacingA))) / stepsA;
								}
								else
								{
									hitSpacingA = (distanceA * (distanceB / (stepsB * hitSpacingB))) / stepsB;
								}

								for (var i = 0; i < steps; i++)
								{
									currentPositionA = Vector3.MoveTowards(currentPositionA, position, hitSpacingA);
									currentPositionB = Vector3.MoveTowards(currentPositionB, endPosition, hitSpacingB);

									if (connectHits == true)
									{
										hitCache.InvokeQuad(gameObject, preview, priority, pressure, link.Position, link.EndPosition, currentPositionA, currentPositionB, rotation);
									}
									else
									{
										hitCache.InvokeLine(gameObject, preview, priority, pressure, currentPositionA, currentPositionB, rotation);
									}

									link.Position    = currentPositionA;
									link.EndPosition = currentPositionB;
								}
							}

							return;
						}
						else if (connectHits == true)
						{
							hitCache.InvokeQuad(gameObject, preview, priority, pressure, link.Position, link.EndPosition, position, endPosition, rotation);
						}
						else
						{
							hitCache.InvokeLine(gameObject, preview, priority, pressure, position, endPosition, rotation);
						}
					}
					else
					{
						hitCache.InvokeLine(gameObject, preview, priority, pressure, position, endPosition, rotation);
					}
				}
				else
				{
					link = linkPool.Count > 0 ? linkPool.Pop() : new Link();

					link.Owner = owner;

					links.Add(link);

					hitCache.InvokeLine(gameObject, preview, priority, pressure, position, endPosition, rotation);
				}

				link.Position    = position;
				link.EndPosition = endPosition;
				link.Preview     = preview;
			}
			else
			{
				hitCache.InvokeLine(gameObject, preview, priority, pressure, position, endPosition, rotation);
			}
		}

		protected virtual void OnEnable()
		{
			ResetConnections();
		}

		protected virtual void Update()
		{
			for (var i = links.Count - 1; i >= 0; i--)
			{
				var link = links[i];

				link.Age += Time.deltaTime;

				if (link.Age > 1.0f)
				{
					link.Age = 0.0f;

					links.RemoveAt(i);

					linkPool.Push(link);
				}
			}
		}

		private bool TryGetLink(object owner, ref Link link)
		{
			for (var i = links.Count - 1; i >= 0; i--)
			{
				link = links[i];

				link.Age = 0.0f;

				if (link.Owner == owner)
				{
					return true;
				}
			}

			return false;
		}
	}
}

#if UNITY_EDITOR
namespace PaintIn3D
{
	using UnityEditor;

	public class P3dConnectableLines_Editor<T> : P3dEditor<T>
		where T : P3dConnectableLines
	{
		protected override void OnInspector()
		{
			Draw("connectHits", "If you enable this then the hit lines generated by this component will be connected into quads, allowing you to paint continuously.");
			Draw("hitSpacing", "The world space distance between each hit point.\n\n0 = No spacing.");
			Draw("hitLimit", "When using HitSpacing, this prevents scenarios where something goes wrong and you attempt to paint too many times per frame.");
		}
	}
}
#endif