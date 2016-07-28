#region

using System.Xml;

#endregion

namespace EppLib.Entities
{
    public class Poll : EppCommand<PollResponse>
    {
        public string MessageId { get; set; }
        public string Type { get; set; }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var poll = CreateElement(doc, "poll");

            poll.SetAttribute("op", Type);

            if (Type.Equals("ack") && MessageId != null)
            {
                poll.SetAttribute("msgID", MessageId);
            }

            commandRootElement.AppendChild(poll);

            return commandRootElement;
        }

        public override PollResponse FromBytes(byte[] bytes)
        {
            return new PollResponse(bytes);
        }
    }
}