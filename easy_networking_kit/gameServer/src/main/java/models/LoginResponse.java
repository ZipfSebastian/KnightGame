package models;

/**
 * Created by fazek on 2016. 07. 07..
 */
public class LoginResponse {

    private String type;
    private boolean succes;
    private String session;

    public String getSession() {
        return session;
    }

    public void setSession(String session) {
        this.session = session;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public boolean isSucces() {
        return succes;
    }

    public void setSucces(boolean succes) {
        this.succes = succes;
    }
}
