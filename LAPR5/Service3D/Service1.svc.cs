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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
    
        public IList LoadAllEstado_Humor( )
        {
            return Library.Estado_Humor.LoadAll();
        }

        public IList LoadAllPerfil_Esterno()
        {
            return Library.Perfil_Externo.LoadAll();
        }

        public IList LoadAllRede_amigo()
        {
            return Library.Rede_amigo.LoadAll();
        }
        public IList LoadAllTag()
        {
            return Library.Tag.LoadAll();
        }
        public IList LoadAllUtilizador()
        {
            return Library.Utilizador.LoadAll();
        }
    }
}
