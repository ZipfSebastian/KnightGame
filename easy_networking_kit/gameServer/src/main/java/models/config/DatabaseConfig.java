package models.config;

import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;

/**
 * Created by zipfs on 2016. 06. 28..
 */
public class DatabaseConfig {

    @JacksonXmlProperty(localName = "DatabaseURL")
    private String databaseURL;

    public String getDatabaseURL() {
        return databaseURL;
    }

    public void setDatabaseURL(String databaseURL) {
        this.databaseURL = databaseURL;
    }
}
