﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MinecraftRetroBiomes
{
    public enum TYPE : sbyte
    {
        TAG_End = 0,
        TAG_Byte = 1,
        TAG_Short = 2,
        TAG_Int = 3,
        TAG_Long = 4,
        TAG_Float = 5,
        TAG_Double = 6,
        TAG_Byte_Array = 7,
        TAG_String = 8,
        TAG_List = 9,
        TAG_Compound = 10,
        TAG_Int_Array = 11
    }

    public abstract class TAG
    {
        public TYPE Type;
        public TAG_String Name;
        public bool IsNamed = false;
        public abstract void Read(Stream data);
        public abstract void Write(Stream data);
    }

    public class TAG_Byte : TAG
    {
        public SByte Payload;

        public TAG_Byte()
        {
            Payload = 0;
            Type = TYPE.TAG_Byte;
        }

        public TAG_Byte(Stream data)
            : this()
        {
            Read(data);
        }

        public override void Read(Stream data)
        {
            Payload = (SByte)data.ReadByte();
        }

        public override void Write(Stream data)
        {
            data.WriteByte((byte)Payload);
        }

        public override string ToString()
        {
            if (IsNamed)
                return String.Format("TAG_Byte(\"{0}\"): {1}\r\n", Name.PayloadString, Payload);
            else
                return String.Format("TAG_Byte: {0}\r\n", Payload);
        }
    }

    public class TAG_Short : TAG
    {
        public Int16 Payload;

        public TAG_Short()
        {
            Payload = 0;
            Type = TYPE.TAG_Short;
        }

        public TAG_Short(Stream data)
            : this()
        {
            Read(data);
        }

        public override void Read(Stream data)
        {
            byte[] temp = new byte[2];
            data.Read(temp, 0, 2);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(temp);
            Payload = BitConverter.ToInt16(temp, 0);
        }

        public override void Write(Stream data)
        {
            byte[] temp = BitConverter.GetBytes(Payload);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(temp);
            data.Write(temp, 0, 2);
        }

        public override string ToString()
        {
            if (IsNamed)
                return String.Format("TAG_Short(\"{0}\"): {1}\r\n", Name.PayloadString, Payload);
            else
                return String.Format("TAG_Short: {0}\r\n", Payload);
        }
    }

    public class TAG_Int : TAG
    {
        public Int32 Payload;

        public TAG_Int()
        {
            Payload = 0;
            Type = TYPE.TAG_Int;
        }

        public TAG_Int(Stream data)
            : this()
        {
            Read(data);
        }

        public override void Read(Stream data)
        {
            byte[] temp = new byte[4];
            data.Read(temp, 0, 4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(temp);
            Payload = BitConverter.ToInt32(temp, 0);
        }

        public override void Write(Stream data)
        {
            byte[] temp = BitConverter.GetBytes(Payload);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(temp);
            data.Write(temp, 0, 4);
        }

        public override string ToString()
        {
            if (IsNamed)
                return String.Format("TAG_Int(\"{0}\"): {1}\r\n", Name.PayloadString, Payload);
            else
                return String.Format("TAG_Int: {0}\r\n", Payload);
        }
    }

    public class TAG_Long : TAG
    {
        public Int64 Payload;

        public TAG_Long()
        {
            Payload = 0;
            Type = TYPE.TAG_Long;
        }

        public TAG_Long(Stream data)
            : this()
        {
            Read(data);
        }

        public override void Read(Stream data)
        {
            byte[] temp = new byte[8];
            data.Read(temp, 0, 8);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(temp);
            Payload = BitConverter.ToInt64(temp, 0);
        }

        public override void Write(Stream data)
        {
            byte[] temp = BitConverter.GetBytes(Payload);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(temp);
            data.Write(temp, 0, 8);
        }

        public override string ToString()
        {
            if (IsNamed)
                return String.Format("TAG_Long(\"{0}\"): {1}\r\n", Name.PayloadString, Payload);
            else
                return String.Format("TAG_Long: {0}\r\n", Payload);
        }
    }

    public class TAG_Float : TAG
    {
        public Single Payload;

        public TAG_Float()
        {
            Payload = 0.0f;
            Type = TYPE.TAG_Float;
        }

        public TAG_Float(Stream data)
            : this()
        {
            Read(data);
        }

        public override void Read(Stream data)
        {
            byte[] temp = new byte[4];
            data.Read(temp, 0, 4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(temp);
            Payload = BitConverter.ToSingle(temp, 0);
        }

        public override void Write(Stream data)
        {
            byte[] temp = BitConverter.GetBytes(Payload);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(temp);
            data.Write(temp, 0, 4);
        }

        public override string ToString()
        {
            if (IsNamed)
                return String.Format("TAG_Float(\"{0}\"): {1}\r\n", Name.PayloadString, Payload);
            else
                return String.Format("TAG_Float: {0}\r\n", Payload);
        }
    }

    public class TAG_Double : TAG
    {
        public Double Payload;

        public TAG_Double()
        {
            Payload = 0.0d;
            Type = TYPE.TAG_Double;
        }

        public TAG_Double(Stream data)
            : this()
        {
            Read(data);
        }

        public override void Read(Stream data)
        {
            byte[] temp = new byte[8];
            data.Read(temp, 0, 8);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(temp);
            Payload = BitConverter.ToDouble(temp, 0);
        }

        public override void Write(Stream data)
        {
            byte[] temp = BitConverter.GetBytes(Payload);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(temp);
            data.Write(temp, 0, 8);
        }

        public override string ToString()
        {
            if (IsNamed)
                return String.Format("TAG_Double(\"{0}\"): {1}\r\n", Name.PayloadString, Payload);
            else
                return String.Format("TAG_Double: {0}\r\n", Payload);
        }
    }

    public class TAG_Byte_Array : TAG
    {
        public TAG_Int Length;
        public Byte[] Payload;

        public TAG_Byte_Array()
        {
            Length = null;
            Payload = null;
            Type = TYPE.TAG_Byte_Array;
        }

        public TAG_Byte_Array(Stream data)
            : this()
        {
            Read(data);
        }

        public override void Read(Stream data)
        {
            Length = new TAG_Int(data);

            Payload = new Byte[Length.Payload];
            for (Int32 i = 0; i < Length.Payload; i++)
                Payload[i] = (Byte)data.ReadByte();
        }

        public override void Write(Stream data)
        {
            Length.Write(data);
            data.Write(Payload, 0, Payload.Length);
        }

        public override string ToString()
        {
            if (IsNamed)
                return String.Format("TAG_Byte_Array(\"{0}\"): [{1} bytes]\r\n", Name.PayloadString, Length.Payload);
            else
                return String.Format("TAG_Byte_Array: [{0} bytes]", Length.Payload);
        }
    }

    public class TAG_Int_Array : TAG
    {
        public TAG_Int Length;
        public Int32[] Payload;

        public TAG_Int_Array()
        {
            Length = null;
            Payload = null;
            Type = TYPE.TAG_Int_Array;
        }

        public TAG_Int_Array(Stream data)
            : this()
        {
            Read(data);
        }

        public override void Read(Stream data)
        {
            Length = new TAG_Int(data);

            Payload = new Int32[Length.Payload];
            for (Int32 i = 0; i < Length.Payload; i++)
            {
                byte[] temp = new byte[4];
                data.Read(temp, 0, 4);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(temp);
                Payload[i] = BitConverter.ToInt32(temp, 0);
            }
        }

        public override void Write(Stream data)
        {
            Length.Write(data);
            for (Int32 i = 0; i < Length.Payload; i++)
            {
                byte[] temp = BitConverter.GetBytes(Payload[i]);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(temp);
                data.Write(temp, 0, 4);
            }
        }

        public override string ToString()
        {
            if (IsNamed)
                return String.Format("TAG_Int_Array(\"{0}\"): [{1} integers]\r\n", Name.PayloadString, Length.Payload);
            else
                return String.Format("TAG_Int_Array: [{0} integers]", Length.Payload);
        }
    }

    public class TAG_String : TAG
    {
        public TAG_Short Length;
        public Byte[] Payload;
        public String PayloadString;

        public TAG_String()
        {
            Length = null;
            Payload = null;
            PayloadString = null;
            Type = TYPE.TAG_String;
        }

        public TAG_String(Stream data)
            : this()
        {
            Read(data);
        }

        public override void Read(Stream data)
        {
            Length = new TAG_Short(data);

            Payload = new Byte[Length.Payload];
            for (Int16 i = 0; i < Length.Payload; i++)
                Payload[i] = (Byte)data.ReadByte();
            PayloadString = UTF8Encoding.UTF8.GetString(Payload);
        }

        public override void Write(Stream data)
        {
            Length.Write(data);
            data.Write(Payload, 0, Payload.Length);
        }

        public override string ToString()
        {
            if (IsNamed)
                return String.Format("TAG_String(\"{0}\"): {1}\r\n", Name.PayloadString, PayloadString);
            else
                return String.Format("TAG_String: {0}\r\n", PayloadString);
        }
    }

    public class TAG_List : TAG
    {
        public TAG_Byte TagId;
        public TAG_Int Length;
        public TAG[] Payload;

        public TAG_List()
        {
            TagId = null;
            Length = null;
            Payload = null;
            Type = TYPE.TAG_List;
        }

        public TAG_List(Stream data)
            : this()
        {
            Read(data);
        }

        public override void Read(Stream data)
        {
            TagId = new TAG_Byte();
            TagId.Read(data);

            Length = new TAG_Int();
            Length.Read(data);

            Payload = new TAG[Length.Payload];
            for (Int32 i = 0; i < Length.Payload; i++)
            {
                switch ((TYPE)TagId.Payload)
                {
                    case TYPE.TAG_Byte:
                        Payload[i] = (TAG)new TAG_Byte(data);
                        break;
                    case TYPE.TAG_Short:
                        Payload[i] = (TAG)new TAG_Short(data);
                        break;
                    case TYPE.TAG_Int:
                        Payload[i] = (TAG)new TAG_Int(data);
                        break;
                    case TYPE.TAG_Long:
                        Payload[i] = (TAG)new TAG_Long(data);
                        break;
                    case TYPE.TAG_Float:
                        Payload[i] = (TAG)new TAG_Float(data);
                        break;
                    case TYPE.TAG_Double:
                        Payload[i] = (TAG)new TAG_Double(data);
                        break;
                    case TYPE.TAG_Byte_Array:
                        Payload[i] = (TAG)new TAG_Byte_Array(data);
                        break;
                    case TYPE.TAG_String:
                        Payload[i] = (TAG)new TAG_String(data);
                        break;
                    case TYPE.TAG_List:
                        Payload[i] = (TAG)new TAG_List(data);
                        break;
                    case TYPE.TAG_Compound:
                        Payload[i] = (TAG)new TAG_Compound(data);
                        break;
                    case TYPE.TAG_Int_Array:
                        Payload[i] = (TAG)new TAG_Int_Array(data);
                        break;
                    default:
                        throw new Exception("Holy mother of crap!");
                }
            }
        }

        public override void Write(Stream data)
        {
            TagId.Write(data);
            Length.Write(data);

            for (Int32 i = 0; i < Length.Payload; i++)
            {
                Payload[i].Write(data);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (IsNamed)
                sb.AppendFormat("TAG_List(\"{0}\"): {1} entries of type {2}\r\n", Name.PayloadString, Length.Payload, ((TYPE)TagId.Payload));
            else
                sb.AppendFormat("TAG_List: {0} entries of type {1}\r\n", Length.Payload, ((TYPE)TagId.Payload));

            sb.Append("{\r\n");

            for (Int32 i = 0; i < Length.Payload; i++)
            {
                sb.Append(Payload[i].ToString());
            }

            sb.Append("}\r\n");

            return sb.ToString();
        }
    }

    public class TAG_Compound : TAG
    {
        public Dictionary<String, TAG> Payload;

        public TAG_Compound()
        {
            Payload = null;
            Type = TYPE.TAG_Compound;
        }

        public TAG_Compound(Stream data)
            : this()
        {
            Read(data);
        }

        public TAG this[String key]
        {
            get
            {
                return Payload[key];
            }
            set
            {
                Payload[key] = value;
            }
        }

        public override void Read(Stream data)
        {
            Payload = new Dictionary<String, TAG>();
            while (true)
            {
                if (data.Position >= data.Length)
                    break;
                TAG_Byte tagType = new TAG_Byte(data);
                if (tagType.Payload == (SByte)TYPE.TAG_End)
                    break;
                TAG_String name = new TAG_String(data);
                TAG nextTag = null;
                switch ((TYPE)tagType.Payload)
                {
                    case TYPE.TAG_Byte:
                        nextTag = (TAG)new TAG_Byte(data);
                        break;
                    case TYPE.TAG_Short:
                        nextTag = (TAG)new TAG_Short(data);
                        break;
                    case TYPE.TAG_Int:
                        nextTag = (TAG)new TAG_Int(data);
                        break;
                    case TYPE.TAG_Long:
                        nextTag = (TAG)new TAG_Long(data);
                        break;
                    case TYPE.TAG_Float:
                        nextTag = (TAG)new TAG_Float(data);
                        break;
                    case TYPE.TAG_Double:
                        nextTag = (TAG)new TAG_Double(data);
                        break;
                    case TYPE.TAG_Byte_Array:
                        nextTag = (TAG)new TAG_Byte_Array(data);
                        break;
                    case TYPE.TAG_String:
                        nextTag = (TAG)new TAG_String(data);
                        break;
                    case TYPE.TAG_List:
                        nextTag = (TAG)new TAG_List(data);
                        break;
                    case TYPE.TAG_Compound:
                        nextTag = (TAG)new TAG_Compound(data);
                        break;
                    case TYPE.TAG_Int_Array:
                        nextTag = (TAG)new TAG_Int_Array(data);
                        break;
                    default:
                        throw new Exception("Holy mother of crap!");
                }
                nextTag.Name = name;
                nextTag.IsNamed = true;
                Payload.Add(name.PayloadString, nextTag);
            }
        }

        public override void Write(Stream data)
        {
            foreach (KeyValuePair<String, TAG> pair in Payload)
            {
                data.WriteByte((byte)pair.Value.Type);
                if (pair.Value.IsNamed)
                    pair.Value.Name.Write(data);
                pair.Value.Write(data);
            }
            data.WriteByte((byte)TYPE.TAG_End);
        }

        public override string ToString()
        {
            if (Payload.Count == 1 && (!IsNamed || Name.Payload.Length == 0))
            {
                var temp = Payload.GetEnumerator();
                temp.MoveNext();
                return temp.Current.Value.ToString();
            }
            StringBuilder sb = new StringBuilder();
            if (IsNamed)
                sb.AppendFormat("TAG_Compound(\"{0}\"): {1} entries\r\n", Name.PayloadString, Payload.Count);
            else
                sb.AppendFormat("TAG_Compound: {0} entries\r\n", Payload.Count);

            sb.Append("{\r\n");

            foreach (TAG t in Payload.Values)
            {
                sb.Append(t.ToString());
            }

            sb.Append("}\r\n");

            return sb.ToString();
        }
    }
}
