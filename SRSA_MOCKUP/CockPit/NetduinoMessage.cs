using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COCKPIT
{
    public class NetduinoMessage
    {
        private int mensageId;
        private String msg;
        private Boolean recebidaPeloArduino = false;
        private Boolean enviada = false;
        private Boolean prioritaria = false;

        public Boolean Prioritaria
        {
            get { return prioritaria; }
            set { prioritaria = value; }
        }

        public Boolean Enviada
        {
            get { return enviada; }
            set { enviada = value; }
        }

        public String Msg
        {
            get { return msg; }
            set { msg = value; }
        }


        public int MensageId
        {
            get { return mensageId; }
            set { mensageId = value; }
        }

        public Boolean RecebidaPeloArduino
        {
            get { return recebidaPeloArduino; }
            set { recebidaPeloArduino = value; }
        }

        public NetduinoMessage()
        {

        }

        public NetduinoMessage(int id, String message, Boolean prioritaria)
        {
            this.MensageId = id;
            this.Msg =  message;
            this.Prioritaria = prioritaria;
        }
    }
}
