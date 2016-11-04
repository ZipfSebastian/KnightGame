package responses;

/**
 * Created by Kesze on 2016.08.27..
 */
public class SearchResponse {
    private boolean success;
    private String message;
    private boolean matchFind;
    private String type;

    public boolean isMatchFind() {
        return matchFind;
    }

    public void setMatchFind(boolean matchFind) {
        this.matchFind = matchFind;
    }

    public boolean isSuccess() {
        return success;
    }

    public void setSuccess(boolean success) {
        this.success = success;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }
}
