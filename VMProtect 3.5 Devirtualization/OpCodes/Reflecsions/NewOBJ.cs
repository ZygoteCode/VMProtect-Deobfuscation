﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;


namespace Vika_Anti_VMP_OOP_3._5_Based.Reflecsions
{
    class NewObj : IOpCode
    {
        public override bool nop_before()
        {
            return true;
        }

        public override bool is_specially()
        {
            return false;
        }
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
                    try
                    {
                        if (codes_orig[i] != pattern[i]) return false;
                    }
                    catch
                    {

                    }
                }
            }
            else
            {
                return false;
            }



            if (md.Body.Instructions[4].Operand.ToString().Contains("MethodBase")) return true; else return false;


        }

        public override int Op_Size()
        {
            return 0;
        }
        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {
            uint token = Convert.ToUInt32(vika.value_stack.Pop());
            return new Vika_Instruction(OpCodes.Newobj, (IMethodDefOrRef)vika.module.ResolveToken(Convert.ToUInt32(token)));
        }

        List<OpCode> pattern = new List<OpCode>() {
            OpCodes.Ldarg,
            OpCodes.Ldarg,
            OpCodes.Call,
            OpCodes.Callvirt,

             OpCodes.Call,
            OpCodes.Stloc,
            OpCodes.Ldarg,
            OpCodes.Ldloc,


             OpCodes.Call,
            OpCodes.Stloc,
            OpCodes.Ldloc,
            OpCodes.Brfalse,

            OpCodes.Ldarg,
            OpCodes.Ldloc,
            OpCodes.Call,
                        OpCodes.Ret,



        };
    }
}
