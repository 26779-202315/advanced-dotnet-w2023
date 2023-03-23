namespace Week10_5_Observer
{
    internal class Program
    {
        public class Subject
        {
            private readonly List<SimpleObserver> observers;

            public Subject()
            {
                observers = new List<SimpleObserver>();
            }

            public void AddObserver(SimpleObserver observer)
            {
                observers.Add(observer);
            }

            public void RemoveObserver(string name)
            {
                observers.RemoveAll(o => o.Name == name);
            }

            //Notification
            public void NotifyAll()
            {

                //Do something
                foreach (var item in observers)
                {
                    item.Notify();
                }
            }
        }



        public abstract class SimpleObserver
        {
            public string Name { get; set; }

            protected SimpleObserver(string name)
            {
                this.Name = name;
            }

            public abstract void Notify();
        }


        public class SimpleObserverA : SimpleObserver
        {
            public SimpleObserverA(string name) : base(name)
            {
            }

            public override void Notify()
            {
                Console.WriteLine("I am observer A");
            }
        }

        public class SimpleObserverB : SimpleObserver
        {
            public SimpleObserverB(string name) : base(name)
            {
            }

            public override void Notify()
            {
                Console.WriteLine("I am observer B");
            }
        }

        static void Main(string[] args)
        {
            Subject subject = new Subject();
            SimpleObserverA observerA = new SimpleObserverA("A");
            SimpleObserverB observerB = new SimpleObserverB("B");


            subject.AddObserver(observerA);
            subject.AddObserver(observerB);

            subject.NotifyAll();
        }

    }
}