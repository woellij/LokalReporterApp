using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Helpers.Behaviors {

    public class EventToCommandBehavior : BindableBehavior<View>
    {
        public static readonly BindableProperty EventNameProperty = BindableProperty.Create<EventToCommandBehavior, string>(p => p.EventName, null);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create<EventToCommandBehavior, ICommand>(p => p.Command, null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create<EventToCommandBehavior, object>(p => p.CommandParameter, null);
        public static readonly BindableProperty EventArgsConverterProperty = BindableProperty.Create<EventToCommandBehavior, IValueConverter>(p => p.EventArgsConverter, null);
        public static readonly BindableProperty EventArgsConverterParameterProperty = BindableProperty.Create<EventToCommandBehavior, object>(p => p.EventArgsConverterParameter, null);

        private Delegate handler;
        private EventInfo eventInfo;

        public string EventName
        {
            get { return (string)this.GetValue(EventNameProperty); }
            set { this.SetValue(EventNameProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }

        public IValueConverter EventArgsConverter
        {
            get { return (IValueConverter)this.GetValue(EventArgsConverterProperty); }
            set { this.SetValue(EventArgsConverterProperty, value); }
        }

        public object EventArgsConverterParameter
        {
            get { return this.GetValue(EventArgsConverterParameterProperty); }
            set { this.SetValue(EventArgsConverterParameterProperty, value); }
        }

        protected override void OnAttachedTo(View visualElement)
        {
            base.OnAttachedTo(visualElement);

            var events = this.AssociatedObject.GetType().GetRuntimeEvents().ToArray();
            if (events.Any())
            {
                this.eventInfo = events.FirstOrDefault(e => e.Name == this.EventName);
                if (this.eventInfo == null)
                    throw new ArgumentException(String.Format("EventToCommand: Can't find any event named '{0}' on attached type", this.EventName));

                this.AddEventHandler(this.eventInfo, this.AssociatedObject, this.OnFired);
            }
        }

        protected override void OnDetachingFrom(View view)
        {
            if (this.handler != null)
                this.eventInfo.RemoveEventHandler(this.AssociatedObject, this.handler);

            base.OnDetachingFrom(view);
        }

        private void AddEventHandler(EventInfo eventInfo, object item, Action<object, EventArgs> action)
        {
            var eventParameters = eventInfo.EventHandlerType
                .GetRuntimeMethods().First(m => m.Name == "Invoke")
                .GetParameters()
                .Select(p => Expression.Parameter(p.ParameterType))
                .ToArray();

            var actionInvoke = action.GetType()
                .GetRuntimeMethods().First(m => m.Name == "Invoke");

            this.handler = Expression.Lambda(
                eventInfo.EventHandlerType,
                Expression.Call(Expression.Constant(action), actionInvoke, eventParameters[0], eventParameters[1]),
                eventParameters
                )
                .Compile();

            eventInfo.AddEventHandler(item, this.handler);
        }

        private void OnFired(object sender, EventArgs eventArgs)
        {
            if (this.Command == null)
                return;

            var parameter = this.CommandParameter;

            if (eventArgs != null && eventArgs != EventArgs.Empty)
            {
                parameter = eventArgs;

                if (this.EventArgsConverter != null)
                {
                    parameter = this.EventArgsConverter.Convert(eventArgs, typeof(object), this.EventArgsConverterParameter, CultureInfo.CurrentUICulture);
                }
            }

            if (this.Command.CanExecute(parameter))
            {
                this.Command.Execute(parameter);
            }
        }
    }

}