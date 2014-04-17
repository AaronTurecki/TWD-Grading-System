using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using Recaptcha;


namespace twd_project.Helpers
{
    public static class HtmlHelpers
    {

        const string PUBLIC = "6LedL_ASAAAAABO3NSPoc1W6PLTojzff6CrNAAaC";
        const string PRIVATE = "6LedL_ASAAAAAMvLTe58SBBWKaONprSIsUNfSJJi";

        public static string GenerateCaptcha(this HtmlHelper helper)
        {
            return RecaptchaControlMvc.GenerateCaptcha(helper, "recaptcha", "default");
        }

        public static string GenerateCaptcha(this HtmlHelper helper, string id, string theme)
        {
            if (string.IsNullOrEmpty(PUBLIC) || string.IsNullOrEmpty(PRIVATE))
                throw new ApplicationException(
                "reCAPTCHA needs to be configured with a public & private key.");
            RecaptchaControl recaptchaControl1 = new RecaptchaControl();
            recaptchaControl1.ID = id;
            recaptchaControl1.Theme = theme;
            recaptchaControl1.PublicKey = PUBLIC;
            recaptchaControl1.PrivateKey = PRIVATE;
            RecaptchaControl recaptchaControl2 = recaptchaControl1;
            HtmlTextWriter writer = new HtmlTextWriter((TextWriter)new StringWriter());
            recaptchaControl2.RenderControl(writer);
            return writer.InnerWriter.ToString();
        }
    }

}