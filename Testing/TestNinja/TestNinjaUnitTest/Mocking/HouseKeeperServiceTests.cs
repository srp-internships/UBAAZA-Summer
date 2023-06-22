using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinjaUnitTest.Mocking
{
    [TestFixture]
    public class HouseKeeperServiceTests
    {
        private HouseKeeperService _service;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private DateTime _statementDate=new DateTime(2017,1,1);
        private Housekeeper _houseKeeper;
        private string _statementFileName;

        [SetUp]
        public void SetUp() 
        {
            _houseKeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

            var unitOfWork = new Mock<IUnitOfWork>();

            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
               _houseKeeper

            }.AsQueryable());
            _statementFileName = "fileName";
             _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator.Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, (_statementDate))
                ).Returns(()=>_statementFileName);
            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HouseKeeperService(unitOfWork.Object, 
                _statementGenerator.Object,
                _emailSender.Object,
                _messageBox.Object);

        }
        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);
            _statementGenerator.Verify(sg=>sg.SaveStatement(_houseKeeper.Oid,_houseKeeper.FullName, (_statementDate)));
        }
        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsNull_ShouIdNotGenerateStatement()
        {
            _houseKeeper.Email = null;
            _service.SendStatementEmails(_statementDate);
            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, (_statementDate)),
                Times.Never );
        }
        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsWhitespace_ShouIdNotGenerateStatement()
        {
            _houseKeeper.Email =" ";
            _service.SendStatementEmails(_statementDate);
            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, (_statementDate)),
                Times.Never);
        }
        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsEmpty_ShouIdNotGenerateStatement()
        {
            _houseKeeper.Email = "";
            _service.SendStatementEmails(_statementDate);
            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, (_statementDate)),
                Times.Never);
        }
        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _statementGenerator.Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, (_statementDate))
                ).Returns(_statementFileName);

            _service.SendStatementEmails(_statementDate);
            _emailSender.Verify(es => es.EmailFile(_houseKeeper.Email,_houseKeeper.StatementEmailBody,_statementFileName,It.IsAny<string>()));
        }
        [Test]
        public void SendStatementEmails_StatementFileNameIsNull_ShouldNotEmailTheStatement()
        {
          
            _statementFileName = null;
            _service.SendStatementEmails(_statementDate);
            _emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<string>()),
                Times.Never);
        }
        [Test]
        public void SendStatementEmails_StatementFileNameIsEmptyString_ShouldNotEmailTheStatement()
        {
            _statementFileName = "";

            _service.SendStatementEmails(_statementDate);
            _emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<string>()),
                Times.Never);
        }
        [Test]
        public void SendStatementEmails_StatementFileNameIsWhitespace_ShouldNotEmailTheStatement()
        {
           _statementFileName=" ";

            _service.SendStatementEmails(_statementDate);
            _emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<string>()),
                Times.Never);
        }

    }
}
