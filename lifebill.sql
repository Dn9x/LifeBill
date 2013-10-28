/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50067
Source Host           : localhost:3306
Source Database       : lifebill

Target Server Type    : MYSQL
Target Server Version : 50067
File Encoding         : 65001

Date: 2013-10-28 15:50:47
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `billdetail`
-- ----------------------------
DROP TABLE IF EXISTS `billdetail`;
CREATE TABLE `billdetail` (
  `id` int(11) NOT NULL auto_increment COMMENT '主键ID',
  `masterid` int(11) NOT NULL COMMENT '外键，主档ID',
  `tagid` int(11) NOT NULL COMMENT '外键，tagid',
  `price` double(8,2) NOT NULL COMMENT '价格',
  `notes` text COMMENT '说明',
  `addtime` timestamp NOT NULL default '0000-00-00 00:00:00' on update CURRENT_TIMESTAMP COMMENT '添加时间',
  PRIMARY KEY  (`id`),
  UNIQUE KEY `uq_id` (`id`),
  KEY `fk_masterid` (`masterid`),
  KEY `fk_tagid` (`tagid`),
  CONSTRAINT `fk_masterid` FOREIGN KEY (`masterid`) REFERENCES `billmaster` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_tagid` FOREIGN KEY (`tagid`) REFERENCES `billtags` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of billdetail
-- ----------------------------
INSERT INTO billdetail VALUES ('1', '13', '22', '432.50', 'hahahah', '2013-10-28 14:48:22');
INSERT INTO billdetail VALUES ('2', '32', '5', '145.00', '苏打水', '2013-10-28 15:01:12');
INSERT INTO billdetail VALUES ('3', '32', '7', '30.00', '飞', '2013-10-28 15:01:14');
INSERT INTO billdetail VALUES ('4', '32', '12', '5.00', '稍等', '2013-10-28 15:01:15');
INSERT INTO billdetail VALUES ('5', '32', '11', '8.00', '稍等', '2013-10-28 15:01:17');
INSERT INTO billdetail VALUES ('6', '34', '4', '34.00', '656565', '2013-10-28 15:22:10');
INSERT INTO billdetail VALUES ('7', '34', '7', '30.00', '规划局', '2013-10-28 15:22:12');

-- ----------------------------
-- Table structure for `billmaster`
-- ----------------------------
DROP TABLE IF EXISTS `billmaster`;
CREATE TABLE `billmaster` (
  `ID` int(11) NOT NULL auto_increment COMMENT '主键ID',
  `years` int(11) NOT NULL COMMENT '日期:年月日',
  `months` int(11) NOT NULL,
  `days` int(11) NOT NULL,
  `revenue` double(8,2) NOT NULL,
  `outlay` double(8,2) NOT NULL COMMENT '总金额',
  `addtime` timestamp NOT NULL default '0000-00-00 00:00:00' on update CURRENT_TIMESTAMP COMMENT '添加时间',
  `userid` int(11) NOT NULL COMMENT '用户ID',
  PRIMARY KEY  (`ID`),
  UNIQUE KEY `uq_date` (`years`,`months`,`days`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of billmaster
-- ----------------------------
INSERT INTO billmaster VALUES ('1', '2013', '10', '24', '0.00', '5770.23', '2013-10-25 08:45:33', '1');
INSERT INTO billmaster VALUES ('2', '2013', '10', '1', '0.00', '8589.48', '2013-10-25 08:45:34', '1');
INSERT INTO billmaster VALUES ('3', '2013', '10', '2', '0.00', '5407.30', '2013-10-25 08:45:34', '1');
INSERT INTO billmaster VALUES ('4', '2013', '10', '3', '0.00', '1268.06', '2013-10-25 08:45:36', '1');
INSERT INTO billmaster VALUES ('5', '2013', '10', '4', '0.00', '118.39', '2013-10-25 08:45:37', '1');
INSERT INTO billmaster VALUES ('6', '2013', '10', '5', '0.00', '6787.77', '2013-10-25 08:45:39', '1');
INSERT INTO billmaster VALUES ('7', '2013', '10', '6', '0.00', '3583.70', '2013-10-25 08:45:38', '1');
INSERT INTO billmaster VALUES ('8', '2013', '10', '7', '0.00', '7555.17', '2013-10-25 08:45:42', '1');
INSERT INTO billmaster VALUES ('9', '2013', '10', '8', '0.00', '7024.74', '2013-10-25 08:45:43', '1');
INSERT INTO billmaster VALUES ('10', '2013', '10', '9', '0.00', '2458.19', '2013-10-25 08:45:43', '1');
INSERT INTO billmaster VALUES ('11', '2013', '10', '10', '0.00', '1216.74', '2013-10-25 08:45:58', '1');
INSERT INTO billmaster VALUES ('12', '2013', '10', '11', '0.00', '8709.13', '2013-10-25 08:45:59', '1');
INSERT INTO billmaster VALUES ('13', '2013', '10', '12', '0.00', '9895.42', '2013-10-25 08:46:00', '1');
INSERT INTO billmaster VALUES ('14', '2013', '10', '13', '0.00', '3349.72', '2013-10-25 08:46:00', '1');
INSERT INTO billmaster VALUES ('15', '2013', '10', '14', '0.00', '7062.34', '2013-10-25 08:46:01', '1');
INSERT INTO billmaster VALUES ('16', '2013', '10', '15', '2000.00', '5262.54', '2013-10-25 08:46:21', '1');
INSERT INTO billmaster VALUES ('17', '2013', '10', '16', '0.00', '5125.67', '2013-10-25 08:46:01', '1');
INSERT INTO billmaster VALUES ('18', '2013', '10', '17', '0.00', '9840.75', '2013-10-25 08:46:02', '1');
INSERT INTO billmaster VALUES ('19', '2013', '10', '18', '0.00', '3826.71', '2013-10-25 08:46:02', '1');
INSERT INTO billmaster VALUES ('20', '2013', '10', '19', '0.00', '9611.32', '2013-10-25 08:46:03', '1');
INSERT INTO billmaster VALUES ('21', '2013', '10', '20', '0.00', '6576.45', '2013-10-25 08:46:08', '1');
INSERT INTO billmaster VALUES ('22', '2013', '10', '21', '0.00', '4048.30', '2013-10-25 08:46:08', '1');
INSERT INTO billmaster VALUES ('23', '2013', '10', '22', '0.00', '512.13', '2013-10-25 08:46:09', '1');
INSERT INTO billmaster VALUES ('24', '2013', '10', '23', '0.00', '415.77', '2013-10-25 08:46:09', '1');
INSERT INTO billmaster VALUES ('25', '2013', '10', '25', '0.00', '542.48', '2013-10-25 08:46:10', '1');
INSERT INTO billmaster VALUES ('26', '2013', '10', '26', '0.00', '1465.06', '2013-10-25 08:46:11', '1');
INSERT INTO billmaster VALUES ('27', '2013', '10', '27', '0.00', '5697.87', '2013-10-25 08:46:11', '1');
INSERT INTO billmaster VALUES ('32', '2013', '10', '28', '0.00', '188.00', '2013-10-28 15:01:09', '1');
INSERT INTO billmaster VALUES ('34', '2013', '9', '2', '0.00', '64.00', '2013-10-28 15:22:08', '1');

-- ----------------------------
-- Table structure for `billtags`
-- ----------------------------
DROP TABLE IF EXISTS `billtags`;
CREATE TABLE `billtags` (
  `id` int(11) NOT NULL auto_increment COMMENT '主键ID',
  `tagname` varchar(100) NOT NULL COMMENT 'tag名称',
  `tagtype` varchar(4) NOT NULL,
  `isshow` varchar(2) NOT NULL COMMENT '是否显示',
  `pid` int(11) NOT NULL,
  `addtime` timestamp NOT NULL default '0000-00-00 00:00:00' on update CURRENT_TIMESTAMP COMMENT '添加时间',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of billtags
-- ----------------------------
INSERT INTO billtags VALUES ('1', '外面吃饭', 'O', 'N', '0', '2013-10-28 09:50:24');
INSERT INTO billtags VALUES ('2', '菜钱', 'O', 'N', '0', '2013-10-28 09:49:59');
INSERT INTO billtags VALUES ('3', '日用品', 'O', 'N', '0', '2013-10-28 09:50:00');
INSERT INTO billtags VALUES ('4', '零食', 'O', 'Y', '0', '2013-10-28 09:50:02');
INSERT INTO billtags VALUES ('5', '衣服', 'O', 'Y', '0', '2013-10-28 09:50:07');
INSERT INTO billtags VALUES ('6', '鞋子', 'O', 'Y', '0', '2013-10-28 09:50:07');
INSERT INTO billtags VALUES ('7', '话费', 'O', 'Y', '0', '2013-10-28 09:50:09');
INSERT INTO billtags VALUES ('8', '其他', 'O', 'Y', '0', '2013-10-28 09:50:11');
INSERT INTO billtags VALUES ('9', '请客吃饭', 'O', 'Y', '1', '2013-10-28 09:50:13');
INSERT INTO billtags VALUES ('10', '自己吃饭', 'O', 'Y', '1', '2013-10-28 09:50:14');
INSERT INTO billtags VALUES ('11', '肉类', 'O', 'Y', '2', '2013-10-28 09:50:27');
INSERT INTO billtags VALUES ('12', '菜类', 'O', 'Y', '2', '2013-10-28 09:50:27');
INSERT INTO billtags VALUES ('13', '洗漱用品', 'O', 'Y', '3', '2013-10-28 09:50:29');
INSERT INTO billtags VALUES ('14', '卧室用品', 'O', 'Y', '3', '2013-10-28 09:50:29');
INSERT INTO billtags VALUES ('15', '厨房用品', 'O', 'Y', '3', '2013-10-28 09:50:30');
INSERT INTO billtags VALUES ('16', '柴米油盐', 'O', 'Y', '3', '2013-10-28 09:50:32');
INSERT INTO billtags VALUES ('17', '车费', 'O', 'Y', '0', '2013-10-28 09:50:38');
INSERT INTO billtags VALUES ('18', '学习费用', 'O', 'Y', '0', '2013-10-28 09:50:39');
INSERT INTO billtags VALUES ('19', '水果', 'O', 'Y', '0', '2013-10-28 09:50:44');
INSERT INTO billtags VALUES ('20', '工资', 'I', 'Y', '0', '2013-10-28 09:50:45');
INSERT INTO billtags VALUES ('21', '借钱出去', 'O', 'Y', '0', '2013-10-28 09:50:49');
INSERT INTO billtags VALUES ('22', '别人还钱', 'I', 'Y', '0', '2013-10-28 09:50:52');
INSERT INTO billtags VALUES ('23', '房租', 'O', 'Y', '3', '2013-10-28 09:50:53');
INSERT INTO billtags VALUES ('24', '水费', 'O', 'Y', '3', '2013-10-28 09:50:54');
INSERT INTO billtags VALUES ('25', '电费', 'O', 'Y', '3', '2013-10-28 09:50:55');

-- ----------------------------
-- Table structure for `users`
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `id` int(11) NOT NULL auto_increment COMMENT '主键ID',
  `login` varchar(255) NOT NULL COMMENT '登录账号',
  `pswd` varchar(255) NOT NULL COMMENT '登录密码',
  `name` varchar(255) NOT NULL COMMENT '显示名称',
  `tel` varchar(255) default NULL COMMENT '电话',
  `email` varchar(255) default NULL COMMENT '邮箱',
  `sex` varchar(255) NOT NULL COMMENT '性别',
  `addtime` timestamp NOT NULL default '0000-00-00 00:00:00' on update CURRENT_TIMESTAMP COMMENT '添加时间',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO users VALUES ('1', 'Dn9x', '6A204BD89F3C8348AFD5C77C717A097A', 'Dn9x', '13794829675', 'xiuxu123@live.cn', 'M', '2013-10-25 08:22:39');
