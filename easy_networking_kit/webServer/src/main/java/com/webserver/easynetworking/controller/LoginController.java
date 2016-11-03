package com.webserver.easynetworking.controller;

import ch.qos.logback.core.db.ConnectionSource;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

/**
 * Created by zipfs on 2016. 06. 05..
 */
@RestController
public class LoginController {

    @Value("${database.url}")
    private String dattabaseUrl;

    @RequestMapping("/login/loginUser")
    public ResponseEntity<Void> loginUser(@RequestParam(name = "userName") String uName,
                                          @RequestParam(name = "password") String uPass) {
        try {
            //ConnectionSource
        } catch (Exception e) {

        }
        return null;
    }
}
