package com.webserver.easynetworking;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

/**
 * Created by zipfs on 2016. 06. 04..
 */

@RestController
public class HelloController {

    @RequestMapping("/hello")
    public String index() {
        return "";
    }

}