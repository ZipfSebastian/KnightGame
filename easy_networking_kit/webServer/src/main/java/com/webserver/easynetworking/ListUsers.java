package com.webserver.easynetworking;

import com.j256.ormlite.dao.Dao;
import com.j256.ormlite.dao.DaoManager;
import com.j256.ormlite.jdbc.JdbcConnectionSource;
import com.j256.ormlite.support.ConnectionSource;
import models.SystemUser;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by zipfs on 2016. 06. 04..
 */
@RestController
public class ListUsers {
    @Value("${database.url}")
    private String databaseURL;

    @RequestMapping(value="/listUsers", method = RequestMethod.GET)
    @ResponseBody
    public List<SystemUser> listUsers(){
        try {
            ConnectionSource connectionSource = new JdbcConnectionSource(databaseURL);
            Dao<SystemUser, String> userDao = DaoManager.createDao(connectionSource, SystemUser.class);
            return userDao.queryForAll();
        }catch (Exception e){
            return null;
        }
    }
}
