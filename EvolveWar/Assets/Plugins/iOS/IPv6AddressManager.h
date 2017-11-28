//
//  IPv6AddressManager.h
//  Test
//
//  Created by dd on 2017/10/18.
//  Copyright © 2017年 dd. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface IPv6AddressManager : NSObject
+(NSString *)getIPv6 : (const char *)mHost :(const char *)mPort;
+(void) callIOSSetting;
@end
