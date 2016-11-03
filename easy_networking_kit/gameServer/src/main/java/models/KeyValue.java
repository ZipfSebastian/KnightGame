package models;


import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlRootElement;

/**
 * Created by zipfs on 2016. 01. 09..
 */
@JacksonXmlRootElement(localName = "Variable")
public class KeyValue {

    @JacksonXmlProperty(localName = "Key")
    private String key;
    @JacksonXmlProperty(localName = "Value")
    private String value;

    public KeyValue(String key, String value) {
        this.key = key;
        this.value = value;
    }

    public KeyValue() {
    }

    public String getKey() {
        return key;
    }

    public void setKey(String key) {
        this.key = key;
    }

    public String getValue() {
        return value;
    }

    public void setValue(String value) {
        this.value = value;
    }
}
