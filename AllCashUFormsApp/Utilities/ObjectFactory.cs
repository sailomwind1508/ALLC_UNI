using AllCashUFormsApp.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp
{
    class ObjectFactory
    {
        private ObjectType _type;
        private Dictionary<string, object> _data;

        public IObject Get(ObjectType type, Dictionary<string, object> data)
        {
            _type = type;
            _data = data;

            return GetObject();
        }

        private IObject GetObject()
        {
            IObject obj = null;

            //string code = Convert.ToString(_data["Code"]);
            switch (_type)
            {
                case ObjectType.Promotion:
                    {
                        obj = new Promotion();
                    }
                    break;
                case ObjectType.PromotionTemp:
                    {
                        obj = new PromotionTemp();
                    }
                    break;
                case ObjectType.Supplier:
                    {
                        obj = new Supplier();
                    }
                    break;
                case ObjectType.ODProduct:
                    {
                        obj = new Product();
                    }
                    break;
                case ObjectType.REProduct:
                    {
                        obj = new Product();
                    }
                    break;
                case ObjectType.RLProduct:
                    {
                        obj = new Product();
                    }
                    break;
                case ObjectType.RBProduct:
                    {
                        obj = new Product();
                    }
                    break;
                case ObjectType.RJProduct:
                    {
                        obj = new Product();
                    }
                    break;
                case ObjectType.RTProduct:
                    {
                        obj = new Product();
                    }
                    break;
                case ObjectType.TRProduct:
                    {
                        obj = new Product();
                    }
                    break;
                case ObjectType.IVProduct:
                    {
                        obj = new Product();
                    }
                    break;
                case ObjectType.IMProduct:
                    {
                        obj = new Product();
                    }
                    break;
                case ObjectType.VEProduct:
                    {
                        obj = new Product();
                    }
                    break;
                case ObjectType.IVPreProduct:
                    {
                        obj = new Product();
                    }
                    break;
                case ObjectType.IMPreProduct:
                    {
                        obj = new Product();
                    }
                    break;
                case ObjectType.PreOrderProduct:
                    {
                        obj = new Product();
                    }
                    break;
                case ObjectType.OD:
                    {
                        obj = new OD();
                    }
                    break;
                case ObjectType.REOD:
                    {
                        obj = new OD();
                    }
                    break;
                case ObjectType.RE:
                    {
                        obj = new RE();
                    }
                    break;
                case ObjectType.RL:
                    {
                        obj = new RL();
                    }
                    break;
                case ObjectType.RB:
                    {
                        obj = new RB();
                    }
                    break;
                case ObjectType.RJ:
                    {
                        obj = new RJ();
                    }
                    break;
                case ObjectType.RT:
                    {
                        obj = new RT();
                    }
                    break;
                case ObjectType.TR:
                    {
                        obj = new TR();
                    }
                    break;
                case ObjectType.RJRB:
                    {
                        obj = new RB();
                    }
                    break;
                case ObjectType.BranchWarehouse:
                    {
                        obj = new BranchWarehouse();
                    }
                    break;
                case ObjectType.FromBranchID:
                    {
                        obj = new Branch();
                    }
                    break;
                case ObjectType.Employee:
                    {
                        obj = new Employee();
                    }
                    break;
                case ObjectType.EmployeeName:
                    {
                        obj = new Employee();
                    }
                    break;
                case ObjectType.Customer:
                    {
                        obj = new Customer();
                    }
                    break;
                case ObjectType.CustomerPre:
                    {
                        obj = new Customer();
                    }
                    break;
                case ObjectType.SaleAreaDistrict:
                    {
                        obj = new SaleAreaDistrict();
                    }
                    break;
                case ObjectType.IM: //Van Sales
                    {
                        obj = new IM();
                    }
                    break;
                case ObjectType.IV: //Tablet Sales
                    {
                        obj = new IV();
                    }
                    break;
                case ObjectType.V: //Tablet Sales
                    {
                        obj = new VE();
                    }
                    break;
                case ObjectType.IVPre: //Tablet Sales Pre-Order
                    {
                        obj = new IVPre();
                    }
                    break;
                case ObjectType.IMPre: //Van Sales Pre-Order
                    {
                        obj = new IMPre();
                    }
                    break;
                case ObjectType.PreOrder: //Van Sales Pre-Order
                    {
                        obj = new PreOrder();
                    }
                    break;
                case ObjectType.IVPrePO: //Van Sales Pre-Order
                    {
                        obj = new PreOrder();
                    }
                    break;
                default:
                    {
                        obj = null;
                    }
                    break;
            }

            return obj;
        }
    }
}
