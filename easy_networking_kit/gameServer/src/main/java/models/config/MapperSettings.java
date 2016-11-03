package models.config;

import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlElementWrapper;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;

import java.util.List;

/**
 * Created by zipfs on 2016. 06. 28..
 */

public class MapperSettings {


    @JacksonXmlElementWrapper(useWrapping = false)
    @JacksonXmlProperty(localName = "RequestMap")
    private RequestMap[] requestMap;
}
