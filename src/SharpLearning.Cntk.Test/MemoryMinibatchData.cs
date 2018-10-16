﻿using System;
using System.Linq;

namespace SharpLearning.Cntk.Test
{
    public class MemoryMinibatchData<T>
    {
        public T[] Data;

        /// <summary>
        /// CNTK uses: [W x H x C] layout.
        /// W: Widht
        /// H: Height
        /// C: Channels
        /// </summary>
        public int[] SampleShape;

        /// <summary>
        /// Number of samples in the tensor
        /// </summary>
        public int SampleCount;

        public MemoryMinibatchData(T[] data, int[] sampleShape, int sampleCount)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            SampleShape = sampleShape ?? throw new ArgumentNullException(nameof(sampleShape));

            var totalElements = sampleShape.Aggregate((d1, d2) => d1 * d2) * sampleCount;
            if (totalElements != data.Length)
            {
                throw new ArgumentException($"Data count: {data.Length} does not match " +
                    $" dimensions [{string.Join(", ", sampleShape)}, {sampleCount}]: {totalElements}");
            }

            if (sampleCount < 1) { throw new ArgumentException("Sample count must be at least 1"); }

            Data = data;
            SampleShape = sampleShape;
            SampleCount = sampleCount;
        }

        public MemoryMinibatchData<T> GetSamples(params int[] sampleIndices)
        {
            var sampleSize = SampleShape.Aggregate((d1, d2) => d1 * d2);

            var data = new T[sampleSize * sampleIndices.Length];
            for (int i = 0; i < sampleIndices.Length; i++)
            {
                var sampleIndex = sampleIndices[i];
                var startIndex = sampleIndex * sampleSize;
                Array.Copy(Data, startIndex, data, i * sampleSize, sampleSize);
            }

            return new MemoryMinibatchData<T>(data, SampleShape.ToArray(), sampleIndices.Length);
        }
    }
}
