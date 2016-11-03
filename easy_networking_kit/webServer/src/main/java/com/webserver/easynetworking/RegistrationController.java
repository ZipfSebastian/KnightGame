package com.webserver.easynetworking;

import com.j256.ormlite.dao.Dao;
import com.j256.ormlite.dao.DaoManager;
import com.j256.ormlite.jdbc.JdbcConnectionSource;
import com.j256.ormlite.support.ConnectionSource;
import models.SystemUser;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletRequest;

/**
 * Created by zipfs on 2016. 06. 04..
 */
@RestController
public class RegistrationController {

    @Value("${database.url}")
    private String databaseURL;

    @RequestMapping(value= "/register", method = RequestMethod.POST)
    @ResponseBody
    public String createCustomer(HttpServletRequest request,
                                 @RequestParam(value="txtName", required=false) String name,
                                 @RequestParam(value="txtPassword", required=false) String password,
                                 @RequestParam(value = "txtEmail", required = false) String email) {
        try {
            ConnectionSource connectionSource = new JdbcConnectionSource(databaseURL);
            SystemUser newUser = new SystemUser(email, name,MD5Hash.getMD5(password),0);
            Dao<SystemUser, String> userDao = DaoManager.createDao(connectionSource, SystemUser.class);
            userDao.create(newUser);
        }catch (Exception e){
            return "Registration faild: " + e.getMessage();
        }
        return "Success";
    }
}
