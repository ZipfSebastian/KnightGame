package handlers;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.j256.ormlite.dao.Dao;
import com.j256.ormlite.dao.DaoManager;
import constants.Constants;
import interfaces.RequestHandler;
import helpers.MD5Hash;
import helpers.Session;
import jdk.nashorn.internal.ir.debug.JSONWriter;
import logger.Log;
import models.Client;
import models.LoginResponse;
import models.SystemUser;

import java.util.List;

/**
 * Created by zipfs on 2015. 12. 19..
 */

public class LoginRequest extends RequestHandler {

    private String userName;
    private String password;

    @Override
    public void onRecive(String message) {
        try {
            Dao<SystemUser, String> userDao =
                    DaoManager.createDao(connectionSource, SystemUser.class);

            List<SystemUser> users = userDao.queryBuilder().where().eq(SystemUser.USER_NAME, userName)
                    .and().eq(SystemUser.USER_PASSWORD, MD5Hash.getMD5(password)).query();
            if (users.size()>0){
                Client user = new Client(users.get(0));

                user.setClientThread(thread);
                if (user != null) {
                    String session = Session.addUser(user);
                    LoginResponse response = new LoginResponse();
                    response.setType("LoginResponse");
                    response.setSucces(true);
                    response.setSession(session);
                    send(new ObjectMapper().writeValueAsString(response), Constants.TCP, user);
                }
            }else {
                Client client = new Client();
                client.setClientThread(thread);
                LoginResponse response = new LoginResponse();
                response.setType("LoginResponse");
                response.setSucces(false);
                send(new ObjectMapper().writeValueAsString(response), Constants.TCP, client);
            }
        }catch (Exception e){
            Log.write(e);
        }
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }
}
