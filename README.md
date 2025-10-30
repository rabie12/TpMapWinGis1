springdoc:
  api-docs:
    version: OPENAPI_3_1

import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.info.Info;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class OpenAPIConfig {
    @Bean
    public OpenAPI customOpenAPI() {
        return new OpenAPI()
            .info(new Info()
                .title("OlkyRegister API")
                .version("1.0")
                .description("Business Registry API Documentation")
            )
            .openapi("3.1.0"); // 👈 Force OpenAPI 3.1
    }
}