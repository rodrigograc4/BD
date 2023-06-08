using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto
{
    [Serializable()]
    public class Objeto
    {
        private String _objetoID;
        private String _Nome;
        private String _Localizacao_X;
        private String _Localizacao_Y;
        private String _FK_eraID;
        private String _FK_jogador_ID_tem;
        private String _FK_jogador_ID_elimina;

        public String ObjID
        {
            get { return _objetoID; }
            set
            {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("Objeto ID field can’t be empty");
                }
                _objetoID = value;
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

        public String Localizacao_X
        {
            get { return _Localizacao_X; }
            set { _Localizacao_X = value; }
        }

        public String Localizacao_Y
        {
            get { return _Localizacao_Y; }
            set { _Localizacao_Y = value; }
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

        public String FK_jogador_ID_tem
        {
            get { return _FK_jogador_ID_tem; }
            set
            {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("FK_GrandeID field can’t be empty");
                }
                _FK_jogador_ID_tem = value;
            }
        }

        public String FK_jogador_ID_elimina
        {
            get { return _FK_jogador_ID_elimina; }
            set
            {
                _FK_jogador_ID_elimina = value;
            }
        }


        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_objetoID + "   ");
            sb.Append(_Nome);
            if (_Localizacao_X != "" && _Localizacao_Y != "")
                sb.Append("  ( " + _Localizacao_X + ", " + _Localizacao_Y + " )");
            return sb.ToString();
        }
    }
}
