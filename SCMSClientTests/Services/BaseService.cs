using NUnit.Framework;
using SCMSClient.Services.Implementation;
using System;

namespace SCMSClientTests.Services
{
    [TestFixture]
    public abstract class BaseService<T>
    {
        public AbstractService<T> ServiceClass { get; set; }

        protected abstract void Setup();

        #region Get Method

        [Test]
        public virtual void Get_ParameterIsNull_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => ServiceClass.Get(null));
        }

        [Test]
        public virtual void Get_ParameterIsEmpty_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => ServiceClass.Get(string.Empty));
        }

        [Test]
        public virtual void Get_HTTPThrowsException_EscalatesTheException()
        {
            Assert.Throws<Exception>(() => ServiceClass.Get("exception"));
        }

        [Test]
        public virtual void Get_HTTPReturnsObject_ReturnsNotNull()
        {
            Assert.NotNull(ServiceClass.Get("object"));
        }

        #endregion

        #region Update Method

        [Test]
        public virtual void Put_ParameterIsNull_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => ServiceClass.Update(default(T)));
        }

        [Test]
        public virtual void Put_HTTPThrowsException_EscalatesTheException()
        {
            Assert.Throws<Exception>(() => ServiceClass.Delete("exception"));
        }

        [Test]
        public virtual void Put_HTTPReturnsObject_ReturnsNotNull()
        {
            Assert.NotNull(ServiceClass.Delete("object"));
        }

        #endregion

        #region Create Method

        [Test]
        public virtual void Post_ParameterIsNull_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => ServiceClass.Create(default(T)));
        }

        [Test]
        public virtual void Post_HTTPThrowsException_EscalatesTheException()
        {
            Assert.Throws<Exception>(() => ServiceClass.Delete("exception"));
        }

        [Test]
        public virtual void Post_HTTPReturnsObject_ReturnsNotNull()
        {
            Assert.NotNull(ServiceClass.Delete("object"));
        }

        #endregion

        #region Delete Method

        [Test]
        public virtual void Delete_ParameterIsNull_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => ServiceClass.Delete(null));
        }

        [Test]
        public virtual void Delete_ParameterIsEmpty_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => ServiceClass.Delete(string.Empty));
        }

        [Test]
        public virtual void Delete_HTTPThrowsException_EscalatesTheException()
        {
            Assert.Throws<Exception>(() => ServiceClass.Delete("exception"));
        }

        [Test]
        public virtual void Delete_HTTPReturnsObject_ReturnsNotNull()
        {
            Assert.NotNull(ServiceClass.Delete("object"));
        }

        #endregion

        #region GetAll Method

        [Test]
        public virtual void GetAll_HTTPThrowsException_EscalatesTheException()
        {
            Assert.Throws<Exception>(() => ServiceClass.Get("exception"));
        }

        [Test]
        public virtual void GetAll_HTTPReturnsObject_ReturnsNotNull()
        {
            Assert.NotNull(ServiceClass.Get("object"));
        }

        #endregion
    }
}
