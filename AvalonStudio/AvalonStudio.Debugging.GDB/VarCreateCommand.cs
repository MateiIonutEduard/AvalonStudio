﻿namespace AvalonStudio.Debugging.GDB
{
    public class VarCreateCommand : Command<GDBResponse<VariableObject>>
    {
        public override int TimeoutMs
        {
            get
            {
                return DefaultCommandTimeout;
            }
        }

        public VarCreateCommand(string id, VariableObjectType type, string expression)
        {
            char typeChar = '@';

            if (type == VariableObjectType.Fixed)
            {
                typeChar = '*';
            }

            this.expression = expression;
            commandText = string.Format("-var-create {0} {1} {2}", id, typeChar, expression);
        }

        private string commandText;
        private string expression;

        public override string Encode()
        {
            return commandText;
        }

        protected override GDBResponse<VariableObject> Decode(string response)
        {
            var result = new GDBResponse<VariableObject>(DecodeResponseCode(response));

            if (result.Response == ResponseCode.Done)
            {
                result.Value = VariableObject.FromDataString(null, response.Substring(6), expression);
            }

            return result;
        }

        public override void OutOfBandDataReceived(string data)
        {
            //throw new NotImplementedException ();
        }
    }
}
