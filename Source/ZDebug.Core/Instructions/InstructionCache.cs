﻿using System;
using System.Collections.Generic;
using ZDebug.Core.Collections;

namespace ZDebug.Core.Instructions
{
    internal sealed class InstructionCache
    {
        private readonly Dictionary<int, Instruction> map;

        private Operand[] operandArray = new Operand[1024];
        private int operandArrayFreeIndex;
        private int operandArraySize = 1024;

        private ushort[] zwordArray = new ushort[1024];
        private int zwordArrayFreeIndex;
        private int zwordArraySize = 1024;

        public InstructionCache(int capacity = 0)
        {
            map = new Dictionary<int, Instruction>(capacity);
        }

        public bool TryGet(int address, out Instruction instruction)
        {
            return map.TryGetValue(address, out instruction);
        }

        public void Add(int address, Instruction instruction)
        {
            map.Add(address, instruction);
        }

        public ReadOnlyArray<Operand> AllocateOperands(int length)
        {
            if (length == 0)
            {
                return ReadOnlyArray<Operand>.Empty;
            }

            if (operandArrayFreeIndex > operandArraySize - length)
            {
                var newSize = operandArray.Length * 2;
                var newOperandArray = new Operand[newSize];
                Array.Copy(operandArray, 0, newOperandArray, 0, operandArray.Length);
                operandArray = newOperandArray;
                operandArraySize = newSize;
            }

            var result = new ReadOnlyArray<Operand>(operandArray, operandArrayFreeIndex, length);
            operandArrayFreeIndex += length;
            return result;
        }

        public ReadOnlyArray<ushort> AllocateZWords(int length)
        {
            if (length == 0)
            {
                return ReadOnlyArray<ushort>.Empty;
            }

            if (zwordArrayFreeIndex > zwordArraySize - length)
            {
                var newSize = zwordArray.Length * 2;
                var newZWordsArray = new ushort[newSize];
                Array.Copy(zwordArray, 0, newZWordsArray, 0, zwordArray.Length);
                zwordArray = newZWordsArray;
                zwordArraySize = newSize;
            }

            var result = new ReadOnlyArray<ushort>(zwordArray, zwordArrayFreeIndex, length);
            zwordArrayFreeIndex += length;
            return result;
        }
    }
}
