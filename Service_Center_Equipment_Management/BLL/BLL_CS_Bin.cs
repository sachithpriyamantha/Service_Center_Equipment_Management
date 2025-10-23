using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Center_Equipment_Management.BLL
{
    class BLL_CS_Bin
    {
        DAL.DAL_CS_Bin DAL_CS_Bin = new DAL.DAL_CS_Bin();

        internal DataTable Select_Material()
        {
            return DAL_CS_Bin.Select_Material();
        }
        internal DataTable Select_Transaction(string MatCode)
        {
            return DAL_CS_Bin.Select_Transaction(MatCode);
        }
        internal DataTable Select_All_Transaction()
        {
            return DAL_CS_Bin.Select_All_Transaction();
        }
    }
}
