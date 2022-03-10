using System;

namespace Farious.Gist.UIComponents.Components
{
	public class TransformerComponent<TContent, THandledContent> : ProviderComponent<TContent>
	{
		protected IProviderComponent<THandledContent> _parent;
		private Func<THandledContent, TContent> _transformationHandler = null;
		public override TContent Content => _transformationHandler(_parent.Content);

		public TransformerComponent()
		{
			AttachedToNode += () =>
			{
				_parent = Node.FindInParents<IProviderComponent<THandledContent>>(true);
				_parent.ContentChanged += MarkContentAsChanged;
				_parent.ContentModified += MarkAsModified;
			};
		}

		public void SetTransformationHandler(Func<THandledContent, TContent> handler)
		{
			_transformationHandler = handler;
		}
	}
}