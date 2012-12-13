using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Collections;

namespace Service3D
{
   
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        IList LoadAllEstado_Humor();

        [OperationContract]
        IList LoadAllPerfil_Esterno();

        [OperationContract]
        IList LoadAllRede_amigo();

        [OperationContract]
        IList LoadAllTag();

        [OperationContract]
        IList LoadAllUtilizador();

    }

}
