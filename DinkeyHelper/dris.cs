using System;
using System.Runtime.InteropServices;	// so we can marshal the DRIS as a block of memory
using System.Text;						// so we can extract / set strings in the DRIS

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public class DRIS
{
    // the first 4 fields are never encrypted
    public byte header1;				// should be set to "DRIS"

    public byte header2;
    public byte header3;
    public byte header4;

    // inputs
    public int size;					// size of this structure

    public int seed1;					// seed for data/dris encryption
    public int seed2;					// as above

    // (maybe encrypted from now on)
    public int function;				// specify only one function

    public int flags;					// options for the function selected. To use more than one OR them together: OPTION1 | OPTION2...
    public uint execs_decrement;		// amount by which to dec execs if we use flag: DEC_MANY_EXECS
    public int data_crypt_key_num;		// number of the key (1-3) that the dongle uses to encrypt or decrypt user data
    public int rw_offset;				// offset in the dongle data area to read or write data
    public int rw_length;				// length of data are to read/write/encrypt/decrypt
    public IntPtr DoNotUse;				// do not use the rw_data_ptr element use the "Data" argument of the DDProtCheck function

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // NB access this field by using alg_licence_name (see below)
    private byte[] _alt_licence_name = null; // protection check for different licence instead of this one

    public int var_a;					// variable values for user algorithm
    public int var_b;
    public int var_c;
    public int var_d;
    public int var_e;
    public int var_f;
    public int var_g;
    public int var_h;
    public int alg_number;				// the number of the user algorithm that you want to execute

    // outputs
    public int ret_code;				// return code from the protection check

    public int ext_err;					// extended error
    public int type;					// type of dongle detected. 1 = Pro, 2 = FD
    public int model;					// model of dongle detected. 1 = Lite, 2 = Plus, 4 = Net 5, 7 = Net Unlimited
    public int sdsn;					// Software Developer's Serial Number

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]	// NB access this field by using prodcode (see below)
    private byte[] _prodcode = null;	// product code (null-terminated)

    public uint dongle_number;
    public int update_number;
    public uint data_area_size;			// size of the data area in the dongle detected
    public int max_alg_num;				// number of algorithms in the dongle detected
    public int execs;					// executions left: -1 indicates 'no limit'
    public int exp_day;					// expiry day: -1 indicates 'no limit'
    public int exp_month;				// expiry month: -1 indicates 'no limit'
    public int exp_year;				// expiry year: -1 indicates 'no limit'
    public uint features;				// features value
    public int net_users;				// maximum number of network users for the dongle detected: -1 indicates 'mo limit'
    public int alg_answer;				// answer to the user algorithm executed with the given variable values
    public uint fd_capacity;			// capacity of the data area in FD dongle. Currently fixed at ~10MB but may change in the future.

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]	// NB access this field by using fd_drive (see below)
    private byte[] _fd_drive = null;	// fd drive letter (null-terminated)

    public int swkey_type;				// 0 = no swkey detected, 1 = temporary software key, 2 = demo software key
    public int swkey_exp_day;			// software key expiry date (if software key detected)
    public int swkey_exp_month;
    public int swkey_exp_year;

    // NB we have to do it this way because we cannot encrypt strings correctly unless they are byte arrays
    public string prodcode
    {
        get
        {
            StringBuilder sb = new StringBuilder(12);
            foreach (byte b in _prodcode)
            {
                if (b == 0)
                    return sb.ToString();
                else
                    sb.Append((char)b);
            }
            return sb.ToString();
        }
    }

    public string fd_drive
    {
        get
        {
            StringBuilder sb = new StringBuilder(128);
            foreach (byte b in _fd_drive)
            {
                if (b == 0)
                    return sb.ToString();
                else
                    sb.Append((char)b);
            }
            return sb.ToString();
        }
    }

    // NB we have to do it this way because we cannot encrypt strings correctly unless they are byte arrays
    public string alt_licence_name
    {
        set
        {
            int i;
            StringBuilder sb = new StringBuilder(value, 256);
            for (i = 0; i < sb.Length; i++)
            {
                _alt_licence_name[i] = (byte)sb[i];
            }
            _alt_licence_name[i] = 0;				// null terminate
        }
    }

    // sets 4 bytes from an integer at the specified offset in a data array
    public void Set4Bytes(byte[] data, int offset, int value)
    {
        data[offset] = (byte)(value & 0xff);
        data[offset + 1] = (byte)((value >> 8) & 0xff);
        data[offset + 2] = (byte)((value >> 16) & 0xff);
        data[offset + 3] = (byte)((value >> 24) & 0xff);
        return;
    }

    // converts to DRIS structure to a byte array (so we can do encryption)
    public void DrisToByteArray(DRIS dris, byte[] data)
    {
        GCHandle MyGC = GCHandle.Alloc(data, GCHandleType.Pinned);
        Marshal.StructureToPtr(dris, MyGC.AddrOfPinnedObject(), false);
        MyGC.Free();
        return;
    }

    // converts a byte array to the DRIS structure (so we can do encryption)
    public void ByteArrayToDris(byte[] data, DRIS dris)
    {
        GCHandle MyGC = GCHandle.Alloc(data, GCHandleType.Pinned);
        Marshal.PtrToStructure(MyGC.AddrOfPinnedObject(), dris);
        MyGC.Free();
        return;
    }

    // to make a new instance of this class, initialise every element to a random value and then set the header
    public DRIS()
    {
        Random rnd = new Random();
        Byte[] temp = new Byte[Marshal.SizeOf(this)];
        rnd.NextBytes(temp);
        ByteArrayToDris(temp, this);
        this.header1 = (byte)'D';
        this.header2 = (byte)'R';
        this.header3 = (byte)'I';
        this.header4 = (byte)'S';
    }

    // to display common errors that occur - you will want to change these messages
    public string DisplayError(int ret_code, int ext_err)
    {
        switch (ret_code)
        {
            case 401:
                return "Error! No dongles detected!";

            case 403:
                return "Error! A dongle was detected but it was a different type to the one specified in DinkeyAdd.";

            case 404:
                return "Error! A dongle was detected but it was a different model to those specified in DinkeyAdd.";

            case 409:
                return "Error! The dongle detected has not been programmed by DinkeyAdd.";

            case 410:
                return "Error! A dongle was detected but it has a different Product Code to the one specified in DinkeyAdd.";

            case 411:
                return "Error! The dongle detected does not contain the licence associated with this program.";

            case 413:
                return "Error! This program has not been protected by DinkeyAdd. For guidance please read the DinkeyAdd chapter of the Dinkey manual.";

            case 417:
                return "Error! One or more of the parameters set in the DRIS is incorrect.\nIt could also be caused if you are encrypting the DRIS in your code but did not specify DRIS encryption in DinkeyAdd - or vice versa.";

            case 423:
                return "Error! The number of network users has been exceeded.";

            case 435:
                return "Error! DinkeyServer has not been detected on the network.";

            case 922:
                return "Error! The Software Key has expired.";

            // internal error - cannot load DLL
            case -1:
                return "Error! Cannot call protection check because DLL is not loaded.";

            default:
                return "An error occurred checking the dongle. Error: " + String.Format("{0:D}", ret_code) + "\nExtended Error: " + String.Format("{0:D}", ext_err);
        }
    }
}

// class of useful functions for DDGetNetUserList call
internal class NU_INFO
{
    // this is the size of the NU_INFO "struct" - in this case we implement this as a byte array and use these functions to extract the srings
    public const int NU_INFO_SIZE = (256 + 50 + 256 + 16);

    // routine to extract licenceName string from array of NU_INFO bytes
    public static string GetLicenceName(byte[] nu_info_bytes, int index)
    {
        int i, start;
        StringBuilder sb = new StringBuilder(256);
        start = (index * NU_INFO_SIZE) + 0;

        for (i = start; i < start + 256; i++)
        {
            if (nu_info_bytes[i] == 0)
                return sb.ToString();
            else
                sb.Append((char)nu_info_bytes[i]);
        }
        return sb.ToString();
    }

    // routine to extract userName string from array of NU_INFO bytes
    public static string GetUserName(byte[] nu_info_bytes, int index)
    {
        int i, start;
        StringBuilder sb = new StringBuilder(50);
        start = (index * NU_INFO_SIZE) + 256;

        for (i = start; i < start + 50; i++)
        {
            if (nu_info_bytes[i] == 0)
                return sb.ToString();
            else
                sb.Append((char)nu_info_bytes[i]);
        }
        return sb.ToString();
    }

    // routine to extract computerName string from array of NU_INFO bytes
    public static string GetComputerName(byte[] nu_info_bytes, int index)
    {
        int i, start;
        StringBuilder sb = new StringBuilder(256);
        start = (index * NU_INFO_SIZE) + 256 + 50;

        for (i = start; i < start + 256; i++)
        {
            if (nu_info_bytes[i] == 0)
                return sb.ToString();
            else
                sb.Append((char)nu_info_bytes[i]);
        }
        return sb.ToString();
    }

    // routine to extract ipAddress string from array of NU_INFO bytes
    public static string GetIpAddress(byte[] nu_info_bytes, int index)
    {
        int i, start;
        StringBuilder sb = new StringBuilder(16);
        start = (index * NU_INFO_SIZE) + 256 + 50 + 256;

        for (i = start; i < start + 16; i++)
        {
            if (nu_info_bytes[i] == 0)
                return sb.ToString();
            else
                sb.Append((char)nu_info_bytes[i]);
        }
        return sb.ToString();
    }
}

// 32-bit protection check (loads 32-bit DLL)
internal class DDProtCheck32
{
    [DllImport("dpwin32.dll", CharSet = CharSet.Ansi)]
    public static extern int DDProtCheck([In, Out, MarshalAs(UnmanagedType.LPStruct)]DRIS dris, byte[] data);
}

// 64-bit protection check (loads 64-bit DLL)
internal class DDProtCheck64
{
    [DllImport("dpwin64.dll", CharSet = CharSet.Ansi)]
    public static extern int DDProtCheck([In, Out, MarshalAs(UnmanagedType.LPStruct)]DRIS dris, byte[] data);
}

// 32-bit get network user info (loads 32-bit DLL)
internal class DDGetNetUserList32
{
    [DllImport("dpwin32.dll", CharSet = CharSet.Ansi)]
    public static extern int DDGetNetUserList(string licence_name, out int num_net_users, byte[] nu_info_bytes, int num_info_structs, out int extended_error);
}

// 64-bit get network user info (loads 64-bit DLL)
internal class DDGetNetUserList64
{
    [DllImport("dpwin64.dll", CharSet = CharSet.Ansi)]
    public static extern int DDGetNetUserList(string licence_name, out int num_net_users, byte[] nu_info_bytes, int num_info_structs, out int extended_error);
}

// call our API - we only want to load the correct DLL for the bit-ness of the computer. The other DLL may not exist.
internal class DinkeyPro
{
    // calls the DDProtCheck function in the appropriate DLL
    public static int DDProtCheck(DRIS dris, byte[] data)
    {
        int ret_code = -1;

        if (IntPtr.Size == 4)
        {
            try
            {
                ret_code = DDProtCheck32.DDProtCheck(dris, data);
            }
            catch (DllNotFoundException)
            {
                Console.WriteLine("Error! Cannot find dpwin32.dll. This should be in the same folder as DllTest.");
            }
        }
        else
        {
            try
            {
                ret_code = DDProtCheck64.DDProtCheck(dris, data);
            }
            catch (DllNotFoundException)
            {
                Console.WriteLine("Error! Cannot find dpwin64.dll. This should be in the same folder as DllTest.");
            }
        }
        return ret_code;
    }

    public static int DDGetNetUserList(string licence_name, out int num_net_users, byte[] nu_info_bytes, int num_info_structs, out int extended_error)
    {
        int ret_code = -1;
        extended_error = num_net_users = 0;

        if (IntPtr.Size == 4)
        {
            try
            {
                ret_code = DDGetNetUserList32.DDGetNetUserList(licence_name, out num_net_users, nu_info_bytes, num_info_structs, out extended_error);
            }
            catch (DllNotFoundException)
            {
                Console.WriteLine("Error! Cannot find dpwin32.dll. This should be in the same folder as DllTest.");
            }
        }
        else
        {
            try
            {
                ret_code = DDGetNetUserList64.DDGetNetUserList(licence_name, out num_net_users, nu_info_bytes, num_info_structs, out extended_error);
            }
            catch (DllNotFoundException)
            {
                Console.WriteLine("Error! Cannot find dpwin64.dll. This should be in the same folder as DllTest.");
            }
        }
        return ret_code;
    }
}