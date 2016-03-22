﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    public class GraphIterator
    {
        private readonly short[][] incedenceMatrix;
        private readonly bool[] visitedVertices;
        private readonly List<int> currentSequence;
        private int currentVertex;

        internal event Action<GraphIteratorEventArgs> PreviouslyHitVerticeVisited;

        internal GraphIterator(short[][] incedenceMatrix)
        {
            this.incedenceMatrix = incedenceMatrix;
            visitedVertices = new bool[incedenceMatrix.Length];
            currentSequence = new List<int>();
        }

        internal void IterateAllGraph()
        {
            while (ThereAreNotVisitedVertices())
            {
                currentVertex = visitedVertices.IndexesOf(v => !v).First();
                InspectVertex();
            }
        }

        private bool ThereAreNotVisitedVertices()
        {
            return visitedVertices.Any(v => !v);
        }

        private void InspectVertex()
        {
            if (IsCurrentVertexVisited())
            {
                OnVisitVisitedVertex();
            }
            else
            {
                JumpToNextVertex();
            }
        }

        private bool IsCurrentVertexVisited()
        {
            return visitedVertices[currentVertex];
        }

        private void JumpToNextVertex()
        {
            IncludeInSequence();
            foreach (var arcIndex in FindOutgoingArcIndexes())
            {
                currentVertex = FindeNextVertex(arcIndex);
                InspectVertex();
            }
            ExcludeLastVertexFromSequence();
        }

        private void IncludeInSequence()
        {
            visitedVertices[currentVertex] = true;
            currentSequence.Add(currentVertex);
        }

        private IEnumerable<int> FindOutgoingArcIndexes()
        {
            return incedenceMatrix[currentVertex].IndexesOf(v => v == 1);
        }

        private int FindeNextVertex(int outgoingArcIndex)
        {
            return incedenceMatrix.IndexInColumnOf(outgoingArcIndex, v => v == -1);
        }

        private void ExcludeLastVertexFromSequence()
        {
            currentSequence.RemoveAt(currentSequence.Count - 1);
        }

        internal void IterateSegment(int[] startSequence)
        {
            currentSequence.AddRange(startSequence.Take(startSequence.Length - 1));
            foreach (var i in currentSequence)
            {
                visitedVertices[i] = true;
            }
            currentVertex = startSequence.Last();

            InspectVertex();
        }

        private void OnVisitVisitedVertex()
        {
            var eventArgs = new GraphIteratorEventArgs(visitedVertices, currentSequence, currentVertex);
            PreviouslyHitVerticeVisited?.Invoke(eventArgs);
        }
    }
}