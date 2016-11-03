package models.config;

import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlElementWrapper;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;
import models.KeyValue;

/**
 * Created by sinemissione on 2016.06.29..
 */
public class Variables {

    @JacksonXmlProperty(localName = "Variable")
    @JacksonXmlElementWrapper(useWrapping = false)
    private KeyValue[] variables;

    public KeyValue[] getVariables() {
        return variables;
    }

    public void setVariables(KeyValue[] variables) {
        this.variables = variables;
    }
}
