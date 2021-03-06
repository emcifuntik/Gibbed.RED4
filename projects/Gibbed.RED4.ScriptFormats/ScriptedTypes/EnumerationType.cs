﻿/* Copyright (c) 2020 Rick (rick 'at' gibbed 'dot' us)
 *
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.Collections.Generic;
using System.IO;
using Gibbed.IO;

namespace Gibbed.RED4.ScriptFormats.ScriptedTypes
{
    public class EnumerationType : ScriptedType
    {
        public byte Unknown2A { get; set; }
        public byte Size { get; set; }
        public List<EnumeralType> Enumerals { get; }
        public bool Unknown29 { get; set; }

        public EnumerationType()
        {
            this.Enumerals = new List<EnumeralType>();
        }

        public override ScriptedTypeType Type => ScriptedTypeType.Enumeration;

        internal override void Serialize(Stream output, Endian endian, ICacheTables tables)
        {
            throw new NotImplementedException();
        }

        internal override void Deserialize(Stream input, Endian endian, ICacheTables tables)
        {
            // TODO(gibbed): maybe visibility?
            var unknown2A = input.ReadValueU8();
            var size = input.ReadValueU8();
            var enumeralCount = input.ReadValueU32(endian);
            var enumerals = new EnumeralType[enumeralCount];
            for (uint i = 0; i < enumeralCount; i++)
            {
                var enumeralIndex = input.ReadValueU32(endian);
                enumerals[i] = tables.GetType<EnumeralType>(enumeralIndex);
            }
            var unknown29 = input.ReadValueB8();

            this.Unknown2A = unknown2A;
            this.Size = size;
            this.Enumerals.Clear();
            this.Enumerals.AddRange(enumerals);
            this.Unknown29 = unknown29;
        }
    }
}
