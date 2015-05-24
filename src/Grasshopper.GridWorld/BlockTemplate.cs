using System.Collections.Generic;
using Grasshopper.Assets;

namespace Grasshopper.GridWorld
{
	public class BlockTemplate : Asset
	{
		public BlockTemplate()
		{
			Models = new Dictionary<string, BlockTemplateModel>();
		}

		public Dictionary<string, BlockTemplateModel> Models { get; private set; }

		public BlockTemplateModel WithModel(string id)
		{
			var model = new BlockTemplateModel(this, id, id + "$default");
			Models.Add(model.InstanceId, model);
			return model;
		}
	}

	public class BlockTemplateModel
	{
		internal BlockTemplateModel(BlockTemplate ownerBlockTemplate, string modelId, string instanceId)
		{
			OwnerBlockTemplate = ownerBlockTemplate;
			ModelId = modelId;
			InstanceId = instanceId;
		}

		public BlockTemplate OwnerBlockTemplate { get; set; }
		public string ModelId { get; set; }
		public string InstanceId { get; set; }

		public BlockTemplateModel WithInstanceId(string id)
		{
			InstanceId = id;
			return this;
		}
	}
}