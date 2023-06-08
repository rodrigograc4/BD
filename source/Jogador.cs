using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto
{
    [Serializable()]
    public class Jogador
    {
        private String _jogadorID;
        private String _Nome;
        private String _Clan;
        private String _Cor;
        private String _FK_eraID;
        private String _FK_grandeID;
        private String _FK_equipaID;

        public String JogID
        {
            get { return _jogadorID; }
            set
            {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("Jogador ID field can’t be empty");
                }
                _jogadorID = value;
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

        public String Clan
        {
            get { return _Clan; }
            set { _Clan = value; }
        }

        public String Cor
        {
            get { return _Cor; }
            set { _Cor = value; }
        }
        public String FK_eraID
        {
            get { return _FK_eraID; }
            set
            {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("FK_EraID field can’t be empty");
                }
                _FK_eraID = value;
            }
        }

        public String FK_grandeID
        {
            get { return _FK_grandeID; }
            set
            {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("FK_GrandeID field can’t be empty");
                }
                _FK_grandeID = value;
            }
        }

        public String FK_equipaID
        {
            get { return _FK_equipaID; }
            set
            {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("FK_EquipaID field can’t be empty");
                }
                _FK_equipaID = value;
            }
        }


        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_jogadorID+"   ");
            if (_Clan != "")
                sb.Append("[ "+_Clan+" ]  ");
            sb.Append(_Nome);
            return sb.ToString();
        }
    }
}
