using System;
using System.Runtime.InteropServices;

namespace DinkeyHelper
{
    public abstract class DongleProtectionCheckWithAlgorithm : BaseDongleProtectionCheck
    {
        protected int dris_alg_val_a;
        protected int dris_alg_val_b;
        protected int dris_alg_val_c;
        protected int dris_alg_val_d;
        protected int dris_alg_val_e;
        protected int dris_alg_val_f;
        protected int dris_alg_val_g;
        protected int dris_alg_val_h;

        public DongleProtectionCheckWithAlgorithm()
        {
            SetDrisAlgorithmValues();
        }

        protected abstract Func<int> AlgorithmComputation { get; }

        protected abstract void SetDrisAlgorithmValues();

        public virtual bool CheckProtection(int flags = 0, int alg_num = 1)
        {
            int ret_code;
            var dris = new DRIS();                         // initialise the DRIS with random values & set the header

            dris.size = Marshal.SizeOf(dris);
            dris.function = EXECUTE_ALGORITHM;              // standard protection check & execute algorithm
            dris.flags = flags;                                 // no extra flags, but you may want to specify some if you want to start a network user or decrement execs,...
            dris.alg_number = alg_num;                            // execute algorithm 1 (you do not need to specify this field if you are only using Lite models).
                                                                  // you should remove these entries if you are using Dinkey Lite so that algorithm arguments are random
                                                                  // sample values

            dris.var_a = dris_alg_val_a;
            dris.var_b = dris_alg_val_b;
            dris.var_c = dris_alg_val_c;
            dris.var_d = dris_alg_val_d;
            dris.var_e = dris_alg_val_e;
            dris.var_f = dris_alg_val_f;
            dris.var_g = dris_alg_val_g;
            dris.var_h = dris_alg_val_h;

            ret_code = DinkeyPro.DDProtCheck(dris, null);

            if (ret_code != 0)
            {
                throw new Exception(dris.DisplayError(ret_code, dris.ext_err));
            }

            // later in your code you can check other values in the DRIS...
            if (dris.sdsn != MY_SDSN)
            {
                throw new Exception("Incorrect SDSN! Please modify your source code so that MY_SDSN is set to be your SDSN.");
            }

            // later on in your program you can check the return code again
            if (dris.ret_code != 0)
            {
                throw new Exception("Dinkey Dongle protection error");
            }

            if (AlgorithmComputation() != dris.alg_answer)
            {
                throw new Exception("Dinkey protection error!\nYou have not patched your algorithm in the MyAlgorithm routine");
            }
            return true;
        }
    }
}