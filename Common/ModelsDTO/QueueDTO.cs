using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ModelsDTO
{
    public class QueueDTO
    {
        public int QueueNumber { get; set; }
        public List<MatchQueueDTO> Queues { get; set; }
    }
}
