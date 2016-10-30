namespace OwinModulesHelloWorld
{
    using System.IO;
    using Newtonsoft.Json;
    using Owin;
    using Superscribe.Owin;
    using Superscribe.Owin.Engine;
    using Superscribe.Owin.Extensions;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
            var config = new SuperscribeOwinOptions();
            config.MediaTypeHandlers.Clear();

            config.MediaTypeHandlers.Add(
                "text/html", 
                new MediaTypeHandler { Write = (res, o) => res.WriteResponse( o.ToString()) });

            config.MediaTypeHandlers.Add(
               "application/json",
               new MediaTypeHandler
               {
                   Write = (res, o) => res.WriteResponse(JsonConvert.SerializeObject(o)),
                   Read = (req, type) =>
                   {
                       object obj;
                       using (var reader = new StreamReader(req.GetRequestBody()))
                       {
                           obj = JsonConvert.DeserializeObject(reader.ReadToEnd(), type);
                       };

                       return obj;
                   }
               });

            var engine = OwinRouteEngineFactory.Create(config);
            
            app
                .UseSuperscribeRouter(engine)
                .UseSuperscribeHandler(engine);
        }
    }
}