using System;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Common
{
    /// <summary>
    /// A base class for testing.
    /// 
    /// Provides a basic structure for a test file, and some commonly used logic for tests
    /// </summary>
    [TestClass]
    public abstract class TestBase : IDisposable
    {
        private Random _randomiser;
        private TemporaryFileManager _fileManager;
        private DisposalBag _disposalBag;

        #region Constructors and Destructors

        #region Disposal

        /// <summary>
        ///  Tidies up the managed and unmanaged resources used by this class
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///  Tidies up the managed and unmanaged resources used by this class
        /// </summary>
        /// <param name="disposing">
        ///  true = clean up managed and unmanaged resources
        ///  false = only tidy up unmanaged resources.  This should only be used by finalisers.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
        }

        #endregion

        #endregion

        /// <summary>
        /// Provides a random value
        /// </summary>
        protected Random Randomiser
        {
            get
            {
                if (this._randomiser == null)
                {
                    this._randomiser = new Random((int)DateTime.Now.Ticks);
                }
                return _randomiser;
            }
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        protected TemporaryFileManager FileManager
        {
            get { return _fileManager; }
        }

        protected DisposalBag DisposalBag => _disposalBag;

        #region Setup/Teardown

        [TestInitialize]
        public void TestInitialise()
        {
            _fileManager = new TemporaryFileManager();
            _disposalBag = new DisposalBag();
            _disposalBag.Add(_fileManager);
            this.Setup();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.Teardown();
            _disposalBag.Dispose();
        }

        public virtual void Setup() { }

        public virtual void Teardown() { }

        #endregion


        /// <summary>
        /// Gets a property for a test as defined by the <see cref="TestPropertyAttribute"/> defined on the test method
        /// </summary>
        /// <typeparam name="T">The expected type of the test.  Properties are defined as strings, but could represent other simple data, bool, int, etc.</typeparam>
        /// <param name="key">The name of the property</param>
        /// <param name="defaultValue">The value to return if the property could not be found.</param>
        /// <returns>The value of the property, or <paramref name="defaultValue"/> if it could not be found</returns>
        protected T GetTestProperty<T>(string key, T defaultValue)
        {
            T returnValue = defaultValue;
            if (this.TestContext.Properties.Contains(key))
            {
                returnValue = (T)Convert.ChangeType(this.TestContext.Properties[key], typeof(T));
            }
            return returnValue;
        }

        /// <summary>
        /// This will return a randomly generated string of a given length
        /// </summary>
        /// <param name="size">The length of the string to return</param>
        /// <returns></returns>
        public string RandomString(int size = 35)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Randomiser.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Creates a null-safe string representation of a value for output in messages
        /// </summary>
        /// <param name="value">The value to convert to a string</param>
        /// <returns>The string represemntation of the object</returns>
        protected string ObjectToStringForMessage(object value)
        {
            return value == null ? "<NULL>" : value.ToString();
        }

        #region Resource helpers
        /// <summary>
        /// Reads an embedded resource file from the given name.
        /// </summary>
        /// <param name="resourceName">
        /// The name of the resource.
        /// The name should be the just the extension to the namespace beyond the namespace of the current type
        /// </param>
        /// <returns>The resource as a stream</returns>
        protected Stream GetResourceStream(string resourceName)
        {
            var currentNamespace = this.GetType().Namespace;
            var fullResourceName = string.Format("{0}.{1}", currentNamespace, resourceName);
            var s = this.GetType().Assembly.GetManifestResourceStream(fullResourceName);
            if (s == null)
            {
                // The resource could not be read.
                // Raise a fail assertion, rather than throwing an exception; The exception
                // could be a success condition in a test!
                Assert.Fail("The resource stream {0} could not be read from the assembly", resourceName);
            }
            return s;
        }

        protected string GetResourceFileContentAsString(string resourceName)
        {
            string value;

            using (var reader = new StreamReader(this.GetResourceStream(resourceName)))
            {
                value = reader.ReadToEnd();
            }

            return value;
        }

        protected XmlDocument GetResourceFileAsXmlDocument(string resourceName)
        {
            XmlDocument theDoc;
            using (var valueStream = this.GetResourceStream(resourceName))
            {
                theDoc = new XmlDocument();
                theDoc.Load(valueStream);
            }
            return theDoc;
        }

        #endregion
    }

}
