using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.DocObjects;
using Rhino.DocObjects.Tables;

namespace BetterBlocks.Core
{
    public class BlockWatcher
    {
        // Rhino document this blockwatcher instance is linked to
        private readonly RhinoDoc _active_doc;

        public InstanceDefinition[] InstanceDefinitions { get; private set; } = new InstanceDefinition[0];
        public NestedBlock[] NestedBlocks { get; private set; } = new NestedBlock[0];

        public BlockWatcher(RhinoDoc doc)
        {
            // set linked rhino doc
            _active_doc = doc;

            GetDocumentBlocks();

            RhinoDoc.InstanceDefinitionTableEvent += On_InstanceDefinitionTableEvent;
        }

        private void GetDocumentBlocks()
        {
            InstanceDefinitions = _active_doc.InstanceDefinitions.GetList(true);

            GetNestedBlocks();

            // raise collection changed event
            OnCollectionChanged(new EventArgs());
        }

        private void GetNestedBlocks()
        {
            NestedBlocks = new NestedBlock[InstanceDefinitions.Length];

            for (int i = 0; i < NestedBlocks.Length; i++)
            {
                NestedBlocks[i] = new NestedBlock(_active_doc, InstanceDefinitions[i]);
            }
        }

        private void On_InstanceDefinitionTableEvent(object sender, InstanceDefinitionTableEventArgs e)
        {
            //switch (e.EventType)
            //{
            //    case InstanceDefinitionTableEventType.Added:
            //        break;
            //    case InstanceDefinitionTableEventType.Deleted:
            //        break;
            //    case InstanceDefinitionTableEventType.Undeleted:
            //        break;
            //    case InstanceDefinitionTableEventType.Modified:
            //        break;
            //    case InstanceDefinitionTableEventType.Sorted:
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException();
            //}
            GetDocumentBlocks();
        }

        public event EventHandler CollectionChanged;

        internal void OnCollectionChanged(EventArgs e)
        {
            EventHandler handler = CollectionChanged;
            handler?.Invoke(this, e);
        }
    }
}
