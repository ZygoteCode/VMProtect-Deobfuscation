﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Vika_Anti_VMP_OOP_3._5_Based.VMP
{
    class ConvOvfUn : IOpCode
    {
        public override bool is_opcode(MethodDef md)
        {
            List<OpCode> codes_orig = new List<OpCode>();
            md.Body.SimplifyMacros(null);
            foreach (var instr in md.Body.Instructions)
            {
                codes_orig.Add(instr.OpCode);
            }
            if (codes_orig.Count == pattern.Count)
            {
                for (int i = 0; i < codes_orig.Count; i++)
                {
                    if (codes_orig[i] != pattern[i]) return false;
                }
            }
            else
            {
                return false;
            }

            if (md.Body.Instructions[11].IsLdcI4() && md.Body.Instructions[11].GetLdcI4Value()==1) return true; else return false;

            return false;
        }

        public override int Op_Size()
        {
            return 0;
        }
        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {
            var t = (ITypeDefOrRef)vika.module.ResolveToken(Convert.ToUInt32(vika.value_stack.Pop()));
            switch (t.ToTypeSig().ElementType)
            {
                case ElementType.Boolean: return new Vika_Instruction(OpCodes.Conv_Ovf_I_Un);

                case ElementType.I: return new Vika_Instruction(OpCodes.Conv_Ovf_I_Un);

                case ElementType.I1: return new Vika_Instruction(OpCodes.Conv_Ovf_I1_Un);
                case ElementType.I2: return new Vika_Instruction(OpCodes.Conv_Ovf_I2_Un);
                case ElementType.I4: return new Vika_Instruction(OpCodes.Conv_Ovf_I4_Un);
                case ElementType.I8: return new Vika_Instruction(OpCodes.Conv_Ovf_I8_Un);
                case ElementType.U: return new Vika_Instruction(OpCodes.Conv_Ovf_U_Un);
                case ElementType.U1: return new Vika_Instruction(OpCodes.Conv_Ovf_U1_Un);
                case ElementType.U2: return new Vika_Instruction(OpCodes.Conv_Ovf_U2_Un);
                case ElementType.U4: return new Vika_Instruction(OpCodes.Conv_Ovf_U4_Un);
                case ElementType.U8: return new Vika_Instruction(OpCodes.Conv_Ovf_U8_Un);

                default: Console.WriteLine(t.ToTypeSig().ElementType); throw new Exception();
            }



        }

        List<OpCode> pattern = new List<OpCode>() {
            OpCodes.Ldarg,
           OpCodes.Ldarg,
            OpCodes.Call,
           OpCodes.Callvirt,

             OpCodes.Call,
           OpCodes.Stloc,
            OpCodes.Ldarg,
           OpCodes.Ldarg,

               OpCodes.Ldarg,
           OpCodes.Call,
            OpCodes.Ldloc,
           OpCodes.Ldc_I4,


               OpCodes.Callvirt,
           OpCodes.Ldloc,
            OpCodes.Call,

               OpCodes.Call,
           OpCodes.Ret,
          



        };
        public override bool nop_before()
        {
            return true;
        }

        public override bool is_specially()
        {
            return false;
        }
    }
}
