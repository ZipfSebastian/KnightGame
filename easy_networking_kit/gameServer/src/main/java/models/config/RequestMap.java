package models.config;


import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;

/**
 * Created by zipfs on 2016. 06. 28..
 */

public class RequestMap {

    @JacksonXmlProperty(localName = "From")
    private String from;
    @JacksonXmlProperty(localName = "To")
    private String to;

    public RequestMap() {
    }

    public RequestMap(String from, String to) {
        this.from = from;
        this.to = to;
    }

    public String getFrom() {
        return from;
    }

    public void setFrom(String from) {
        this.from = from;
    }

    public String getTo() {
        return to;
    }

    public void setTo(String to) {
        this.to = to;
    }
}
