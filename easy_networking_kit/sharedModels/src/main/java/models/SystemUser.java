package models;

import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;
import sun.security.pkcs11.wrapper.PKCS11Constants;

import java.io.IOException;
import java.net.DatagramSocket;
import java.security.MessageDigest;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by zipfs on 2015. 12. 19..
 */
@DatabaseTable(tableName = "system__user")
public class SystemUser {

    public static final String USER_ID = "userID";
    public static final String USER_NAME = "name";
    public static final String USER_EMAIL = "email";
    public static final String USER_PASSWORD = "password";
   // public static final String USER_MONEY = "money";

    @DatabaseField(columnName = USER_ID, generatedId = true)
    private int userID;

    @DatabaseField(columnName = USER_EMAIL)
    private String email;

    @DatabaseField(columnName = USER_NAME)
    private String name;

    @DatabaseField(columnName = USER_PASSWORD)
    private String password;

   /* @DatabaseField(columnName = USER_MONEY)
    private int money;*/

    public SystemUser() {

    }

    public SystemUser(String email, String name, String password) {
        this.email = email;
        this.name = name;
        this.password = password;
       // this.money = money;
    }

    public String toMessage() throws JsonProcessingException {
        return new ObjectMapper().writeValueAsString(this);
    }

    public int getUserID() {
        return userID;
    }

    public void setUserID(int userID) {
        this.userID = userID;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

   /* public int getMoney() {
        return money;
    }

    public void setMoney(int money) {
        this.money = money;
    }*/


    public static SystemUser fromJson(String data) throws IOException {
        return new ObjectMapper().readValue(data, SystemUser.class);
    }
}
