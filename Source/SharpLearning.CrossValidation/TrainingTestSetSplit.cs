﻿using SharpLearning.Containers;
using SharpLearning.Containers.Matrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpLearning.CrossValidation
{
    /// <summary>
    /// Container for storing training set/test set split
    /// </summary>
    public sealed class TrainingTestSetSplit : IEquatable<TrainingTestSetSplit>
    {
        public readonly ObservationTargetSet TrainingSet;
        public readonly ObservationTargetSet TestSet;

        /// <summary>
        /// Container for storing training set/test set split.
        /// </summary>
        /// <param name="trainingSet"></param>
        /// <param name="testSet"></param>
        public TrainingTestSetSplit(ObservationTargetSet trainingSet, ObservationTargetSet testSet)
        {
            if (trainingSet == null) { throw new ArgumentNullException("trainingSet"); }
            if (testSet == null) { throw new ArgumentNullException("testSet"); }
            TrainingSet = trainingSet;
            TestSet = testSet;
        }

        /// <summary>
        /// Container for storing training set/test set split.
        /// </summary>
        /// <param name="trainingObservations"></param>
        /// <param name="trainingTargets"></param>
        /// <param name="testObservations"></param>
        /// <param name="testTargets"></param>
        public TrainingTestSetSplit(F64Matrix trainingObservations, double[] trainingTargets, 
            F64Matrix testObservations, double[] testTargets)
            : this(new ObservationTargetSet(trainingObservations, trainingTargets), 
            new ObservationTargetSet(testObservations, testTargets))
        {
        }

        public bool Equals(TrainingTestSetSplit other)
        {
            if (!this.TrainingSet.Equals(other.TrainingSet)) { return false; }
            if (!this.TestSet.Equals(other.TestSet)) { return false; }

            return true;
        }

        public override bool Equals(object obj)
        {
            TrainingTestSetSplit other = obj as TrainingTestSetSplit;
            if (other != null && Equals(other))
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + TrainingSet.GetHashCode();
                hash = hash * 23 + TestSet.GetHashCode();

                return hash;
            }
        }
    }
}
