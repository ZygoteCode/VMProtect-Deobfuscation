﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vika_Anti_VMP_OOP_3._5_Based.Vika_Recompiler_Structures;

using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Vika_Anti_VMP_OOP_3._5_Based.Values
{
    //not realised
    class localloc : IOpCode
    {
        public override bool is_opcode(MethodDef md)
        {
           
                var kk = md.Body.Instructions.Where(sd => sd.OpCode == OpCodes.Call );
                if (kk.Count() >0)
                {
                    if (kk.ToList().Exists(sd => sd.Operand.ToString().Contains("AllocHGlobal")))
                    {
                     

                                return true;
                        
                    }
               

                }
            
            return false;

        }
        public override bool nop_before()
        {
            return false;
        }

        public override bool is_specially()
        {
            return false;
        }

        public override int Op_Size()
        {
            return 0;
        }

        public override Vika_Instruction restore_instruction(BinaryReader read, Vika_Method md, Vika_Context vika)
        {


            //var tok_res = vika.module.ResolveToken(Convert.ToUInt32(token));
            //vika.logger.debug("Disasm token: " + ((ITypeDefOrRef)tok_res).Name);
           // return new Vika_Instruction(OpCodes.Box, ((ITypeDefOrRef)tok_res));

            return new Vika_Instruction(OpCodes.Localloc);

        }




    }
}
