package exporter;

import com.j256.ormlite.dao.Dao;
import com.j256.ormlite.dao.DaoManager;
import com.j256.ormlite.jdbc.JdbcConnectionSource;
import com.j256.ormlite.support.ConnectionSource;
import com.j256.ormlite.table.TableUtils;
import logger.Log;
import models.SystemUser;

import java.util.List;

/**
 * Created by zipfs on 2016. 02. 08..
 */
public class SqliteExporter {

    private String mysqlURL;
    private String sqliteURL;

    public SqliteExporter(String mysqlURL, String sqliteURL) {
        this.mysqlURL = mysqlURL;
        this.sqliteURL = sqliteURL;
    }

    public void createSqliteDB(){
        try {
            ConnectionSource mysqlConnection = new JdbcConnectionSource(mysqlURL);
            ConnectionSource sqliteConnection = new JdbcConnectionSource("jdbc:sqlite:"+sqliteURL);

            /*TEMPLATE with users DON'T USE IT, IT'S NOT SECURE!!*/
            TableUtils.createTableIfNotExists(sqliteConnection, SystemUser.class);
            Dao<SystemUser, String> userDaoSqlite = DaoManager.createDao(sqliteConnection, SystemUser.class);
            List<SystemUser> users = DaoManager.createDao(mysqlConnection, SystemUser.class).queryForAll();
            for(SystemUser user : users){
                List<SystemUser> existingUser = DaoManager.createDao(sqliteConnection, SystemUser.class)
                        .queryBuilder().where().eq(SystemUser.USER_ID, user.getUserID()).query();
                if(existingUser.size()>0){
                    userDaoSqlite.update(user);
                }else{
                    userDaoSqlite.create(user);
                }
            }
            /*TEMPLATE END*/
            Log.write("Export success!!");
        }catch (Exception e){
            Log.write(e);
        }
    }

    public String getMysqlURL() {
        return mysqlURL;
    }

    public void setMysqlURL(String mysqlURL) {
        this.mysqlURL = mysqlURL;
    }

    public String getSqliteURL() {
        return sqliteURL;
    }

    public void setSqliteURL(String sqliteURL) {
        this.sqliteURL = sqliteURL;
    }
}
