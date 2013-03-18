using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class QueueSpecificParams
    {
        /// <summary>
        /// The audit SQS queue that will store audit entries
        /// </summary>
        public string collect_audit_entries { get; set; }

        public ItemAge item_age { get; set; }

        public QueueSize queue_size { get; set; }
    }
}
