using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto
{
    [Serializable()]
    public class Civilizacao
    {
        private String _civID;
        private String _Nome;


        public String CivID
        {
            get { return _civID; }
            set
            {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("Civ ID field can’t be empty");
                }
                _civID = value;
            }
        }


        public String Nome
        {
            get { return _Nome; }
            set
            {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("Nome field can’t be empty");
                }
                _Nome = value;
            }
        }


        public override String ToString()
        {
            return _civID + "   " + _Nome;
        }
    }

    [Serializable()]
    public class Grande_Civilizacao : Civilizacao
    {
        private String _Lider;
        private String _Capital;

        public String Lider
        {
            get { return _Lider; }
            set { _Lider = value; }
        }

        public String Capital
        {
            get { return _Capital; }
            set { _Capital = value; }
        }
    }

    [Serializable()]
    public class Pequena_Civilizacao : Civilizacao
    {
        private String _LimiteTropas;

        public String LimiteTropas
        {
            get { return _LimiteTropas; }
            set { _LimiteTropas = value; }
        }

    }
}
