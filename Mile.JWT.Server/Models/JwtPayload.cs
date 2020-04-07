using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Server.Models
{
    public class JwtPayload
    {
        /// <summary>
        /// subject 主题
        /// </summary>
        public string sub { get; set; }

        /// <summary>
        /// issuer 签发人
        /// </summary>
        public string iss { get; set; }

        public string aud { get; set; }

        /// <summary>
        /// Not Before 生效时间
        /// </summary>
        public string nbf { get; set; }

        /// <summary>
        /// expiration time 过期时间
        /// </summary>
        public string exp { get; set; }

        /// <summary>
        /// Issued At 签发时间
        /// </summary>
        public string iat { get; set; }

        public string claimA { get; set; }

    }
}
