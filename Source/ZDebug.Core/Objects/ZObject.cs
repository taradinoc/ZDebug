﻿using ZDebug.Core.Basics;
using ZDebug.Core.Text;

namespace ZDebug.Core.Objects
{
    public class ZObject
    {
        private readonly Memory memory;
        private readonly ZObjectTable objectTable;
        private readonly ZText ztext;
        private readonly int address;
        private readonly int number;

        internal ZObject(Memory memory, ZObjectTable objectTable, ZText ztext, int address, int number)
        {
            this.memory = memory;
            this.objectTable = objectTable;
            this.ztext = ztext;
            this.address = address;
            this.number = number;
        }

        public int Address
        {
            get { return address; }
        }

        public int Number
        {
            get { return number; }
        }

        public string ShortName
        {
            get
            {
                var shortNameZWords = PropertyTable.GetShortNameZWords();
                return ztext.ZWordsAsString(shortNameZWords, ZTextFlags.All);
            }
        }

        private ZObject GetObjectByNumber(int number)
        {
            return number > 0
                ? objectTable.GetByNumber(number)
                : null;
        }

        public bool HasParent
        {
            get { return memory.ReadParentNumberByObjectAddress(address) != 0; }
        }

        public ZObject Parent
        {
            get { return GetObjectByNumber(memory.ReadParentNumberByObjectAddress(address)); }
        }

        public bool HasSibling
        {
            get { return memory.ReadSiblingNumberByObjectAddress(address) != 0; }
        }

        public ZObject Sibling
        {
            get { return GetObjectByNumber(memory.ReadSiblingNumberByObjectAddress(address)); }
        }

        public bool HasChild
        {
            get { return memory.ReadChildNumberByObjectAddress(address) != 0; }
        }

        public ZObject Child
        {
            get { return GetObjectByNumber(memory.ReadChildNumberByObjectAddress(address)); }
        }

        public bool HasAttribute(int attribute)
        {
            return memory.HasAttributeByObjectAddress(address, attribute);
        }

        public void SetAttribute(int attribute)
        {
            memory.SetAttributeValueByObjectAddress(address, attribute, true);
        }

        public void ClearAttribute(int attribute)
        {
            memory.SetAttributeValueByObjectAddress(address, attribute, false);
        }

        public bool[] GetAllAttributes()
        {
            return memory.GetAllAttributeByObjectAddress(address);
        }

        public ZPropertyTable PropertyTable
        {
            get
            {
                return objectTable.GetPropertyTable(
                    memory.ReadPropertyTableAddressByObjectAddress(address));
            }
        }
    }
}
