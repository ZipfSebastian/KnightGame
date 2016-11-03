package models.config;


import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlElementWrapper;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;

/**
 * Created by zipfs on 2016. 06. 28..
 */
public class LoggerSettings {

    @JacksonXmlProperty(localName = "SendEmail")
    private String sendEmail;
    @JacksonXmlProperty(localName = "Write")
    @JacksonXmlElementWrapper(useWrapping = false)
    private String[] write;

    public String getSendEmail() {
        return sendEmail;
    }

    public void setSendEmail(String sendEmail) {
        this.sendEmail = sendEmail;
    }

    public String[] getWrite() {
        return write;
    }

    public void setWrite(String[] write) {
        this.write = write;
    }
}
