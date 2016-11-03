package models.config;

import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;
import com.sun.org.apache.xpath.internal.operations.Variable;

import javax.xml.crypto.Data;

/**
 * Created by sinemissione on 2016.06.29..
 */
public class GameServerConfig {

    @JacksonXmlProperty(localName = "DatabaseConfig")
    private DatabaseConfig databaseConfig;
    @JacksonXmlProperty(localName = "MailConfig")
    private MailConfig mailConfig;
    @JacksonXmlProperty(localName = "ServerSettings")
    private ServerSettings serverSettings;
    @JacksonXmlProperty(localName = "MapperSettings")
    private MapperSettings mapperSettings;
    @JacksonXmlProperty(localName = "LoggerSettings")
    private LoggerSettings loggerSettings;
    @JacksonXmlProperty(localName = "Variables")
    private Variables variables;

    public DatabaseConfig getDatabaseConfig() {
        return databaseConfig;
    }

    public void setDatabaseConfig(DatabaseConfig databaseConfig) {
        this.databaseConfig = databaseConfig;
    }

    public MailConfig getMailConfig() {
        return mailConfig;
    }

    public void setMailConfig(MailConfig mailConfig) {
        this.mailConfig = mailConfig;
    }

    public ServerSettings getServerSettings() {
        return serverSettings;
    }

    public void setServerSettings(ServerSettings serverSettings) {
        this.serverSettings = serverSettings;
    }

    public MapperSettings getMapperSettings() {
        return mapperSettings;
    }

    public void setMapperSettings(MapperSettings mapperSettings) {
        this.mapperSettings = mapperSettings;
    }

    public LoggerSettings getLoggerSettings() {
        return loggerSettings;
    }

    public void setLoggerSettings(LoggerSettings loggerSettings) {
        this.loggerSettings = loggerSettings;
    }

    public Variables getVariables() {
        return variables;
    }

    public void setVariables(Variables variables) {
        this.variables = variables;
    }
}
