﻿using SharpLearning.Containers.Matrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpLearning.Containers
{
    /// <summary>
    /// Container for storing an observations and targets pair.
    /// </summary>
    public sealed class ObservationTargetSet : IEquatable<ObservationTargetSet>
    {
        public readonly F64Matrix Observations;
        public readonly double[] Targets;

        /// <summary>
        /// Container for storing an observations and targets pair.
        /// </summary>
        /// <param name="observations"></param>
        /// <param name="targets"></param>
        public ObservationTargetSet(F64Matrix observations, double[] targets)
	    {
            if (observations == null) { throw new ArgumentNullException("observations"); }
            if (targets == null) { throw new ArgumentNullException("targets"); }
            Observations = observations;
            Targets = targets;
	    }

        public bool Equals(ObservationTargetSet other)
        {
            if (!this.Observations.Equals(other.Observations)) { return false; }
            if (!this.Targets.SequenceEqual(other.Targets)) { return false; }

            return true;
        }

        public override bool Equals(object obj)
        {
            ObservationTargetSet other = obj as ObservationTargetSet;
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
                hash = hash * 23 + Observations.GetHashCode();
                hash = hash * 23 + Targets.GetHashCode();

                return hash;
            }
        }
    }
}
