using System;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents
{
	public sealed class SelectionComponent
	{
		string SelectedStateClassName
		{
			get => "selected-element";
		}

		SelectionManipulator selectionManipulator;
		HoverManipulator hoverManipulator;

		VisualElement _borderLayer;

		public VisualElement BorderLayer
		{
			get => _borderLayer;
		}

		VisualElement _container;

		public static SelectionComponent SelectedComponent;

		Action _onSelected;
		public void SetOnSelectedHandler(Action handler) => _onSelected = handler;

		Action _onDeselected;
		public void SetOnDeselectedHandler(Action handler) => _onDeselected = handler;

		Action<MouseUpEvent> _onSelection;
		public void SetBeforeSelectedHandler(Action<MouseUpEvent> handler) => _onSelection = handler;

		public void OnSelection(MouseUpEvent e)
		{
			if (_onSelection != null)
				_onSelection(e);

			IsSelected = true;
		}


		bool isSelected = false;

		public bool IsSelected
		{
			get => isSelected;
			set
			{
				if (value)
				{
					if (SelectionComponent.SelectedComponent != this)
					{
						if (SelectionComponent.SelectedComponent != null)
							SelectionComponent.SelectedComponent.IsSelected = false;

						SelectionComponent.SelectedComponent = this;
					}

					if (_onSelected != null)
						_onSelected();

					_container.Focus();
					_borderLayer.EnableInClassList("selectable-element_selected", true);
				}
				else
				{
					if (SelectionComponent.SelectedComponent != this)
						SelectionComponent.SelectedComponent = null;

					if (_onDeselected != null)
						_onDeselected();

					_borderLayer.EnableInClassList("selectable-element_selected", false);
				}

				isSelected = value;
				BorderLayer.EnableInClassList(SelectedStateClassName, value);
			}
		}

		public SelectionComponent(VisualElement container)
		{
			_container = container;

			_borderLayer = new VisualElement();
			_borderLayer.AddToClassList("selectable-element-border-layer");

			_container.Insert(0, _borderLayer);

			selectionManipulator = new SelectionManipulator(container, OnSelection);
			hoverManipulator = new HoverManipulator(container,
				() => _borderLayer.EnableInClassList("selectable-element_hovered", true),
				() => _borderLayer.EnableInClassList("selectable-element_hovered", false));
		}
	}
}