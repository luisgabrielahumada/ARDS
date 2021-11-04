using Common.Infrastructure.Utils;
using Common.Shared;
using Core.Component.Library.SQL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ARDZ.Core
{
    public class Auth : IAuth
    {
        public AppDataBase db;
        public readonly Settings _settings;
        public Auth(Settings settings)
        {
            _settings = settings;
            db = new AppDataBase(_settings.ConnectionStrings.Cnn);
        }

        public Dictionary<string, object> Login(AuthModel auth)
        {
           // var password = new CryptoText().Encriptar(auth.Password, "sgp15081");
            var result = new Execute(db, "DB_Login_Validate",
                                        new
                                        {
                                            v_login = auth.Login,
                                            v_password = auth.Password
                                        }).Procedure<dynamic>()
                                          .FirstOrDefault();
            CheckIf.IsError(result);

            return new Dictionary<string, object> {
                {"userId",result.id.ToString() },
                {"name",result.name.ToString() },
                {"sessionId",result.sessionId.ToString() },
                {"login",result.login.ToString() },
                {"email",result.email.ToString() },
            };
        }
    }
}
